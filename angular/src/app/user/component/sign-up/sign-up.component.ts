import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators, AbstractControl } from '@angular/forms';
import { UserService } from '../../user.service';
import { Router } from '@angular/router';
import { parsePhoneNumberFromString } from 'libphonenumber-js';

export function validatePhoneNumber(control: AbstractControl): ValidationErrors | null {
  if (!control.value) {
    return null; // אם השדה ריק - לא מחזירים שגיאה
  }

  const phoneNumber = parsePhoneNumberFromString(control.value, 'IL'); // IL זה ישראל
  return phoneNumber && phoneNumber.isValid() ? null : { invalidPhone: true };
}

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent implements OnInit {
  public addForm !: FormGroup;
  popupMessage: string = '';
  isPopupVisible = false;

  constructor(private _userService: UserService, private _router: Router) { }

  ngOnInit() {
    this.addForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      address: new FormControl('', Validators.required),
      phone: new FormControl('', [
        Validators.required,
        validatePhoneNumber // שימי לב שאין צורך ב-`this`
      ])
    });
  }

  signUp() {
    console.log(this.addForm.value);
    this._userService.signUp(this.addForm.value).subscribe({
      next: (res) => {
        console.log("המשתמש הצליח להתחבר", res);
        this.popupMessage = `${this.addForm.value.userName}, כמה טוב שנרשמת 🎉 
        <a href="/login">למעבר להתחברות</a>`;
        this.isPopupVisible = true;
      },
      error: (err) => {
        console.log(err);
        if (err.status === 409) { // Conflict - המשתמש כבר קיים
          this.popupMessage = "שם משתמש זה כבר קיים במערכת!"}
        else{
          this.popupMessage = "שגיאת מערכת"
        }
        this.isPopupVisible = true;
      }
    })
  }

}
