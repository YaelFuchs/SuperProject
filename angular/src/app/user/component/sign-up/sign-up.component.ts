import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ValidationErrors, Validators ,AbstractControl} from '@angular/forms';
import { UserService } from '../../user.service';
import { Router } from '@angular/router';
import { parsePhoneNumberFromString } from 'libphonenumber-js';
import { PopupService } from '../../../popup/popup.service';

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
  constructor(private _userService: UserService, private _router: Router,private _popupService: PopupService) { }
  ngOnInit() {
    this.addForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(8)]),
      address: new FormControl('', Validators.required),
      phone : new FormControl('', [
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
        this._popupService.openPopup(
          'נרשמת בהצלחה🎉🎉',
          'מיד תועבר לדף ההתחברות'
        );
        
        setTimeout(() => {
          this._popupService.closePopup();
          this._router.navigate(['/login']);
        }, 3000);
      },
      error: (err) => {
        console.log(err);

      }
    })
  }

}
