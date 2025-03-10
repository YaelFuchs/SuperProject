import { Component, OnInit } from '@angular/core';
import { UserService } from '../../user.service';
import { FormControl, FormGroup, AbstractControl, ValidationErrors, Validators } from '@angular/forms';
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
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.scss']
})
export class UpdateUserComponent implements OnInit {
  public userForm!: FormGroup;
  public userId: number = 0;
  public userData: any = {}; // ברירת מחדל ריקה עד שהנתונים נטענים

  constructor(private _userService: UserService, private _router: Router) { }

  ngOnInit(): void {
    // חילוץ ה-userId מ-localStorage
    const authDataString = localStorage.getItem('authToken');
    if (authDataString) {
      try {
        const authData = JSON.parse(authDataString);
        this.userId = authData.userId || 0;
        console.log('User ID מ-localStorage:', this.userId);
      } catch (error) {
        console.error('שגיאה בפרסור authToken:', error);
        this.userId = 0;
      }
    } else {
      console.log('אין authToken ב-localStorage');
    }

    // יצירת ה-FormGroup מראש עם ערכים ריקים
    this.userForm = new FormGroup({
      userName: new FormControl('', [Validators.required, Validators.minLength(3)]),
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, Validators.minLength(6)]),
      address: new FormControl('', Validators.required),
      phone: new FormControl('', [
        Validators.required,
        validatePhoneNumber // שימי לב שאין צורך ב-`this`
      ])
    });

    // קריאה לשרת לטעינת נתוני המשתמש
    this._userService.getUserById(this.userId).subscribe({
      next: (res) => {
        this.userData = res; // שמירת הנתונים במשתנה
        console.log("תגובת השרת לקריאה לפי id:", res);
        console.log("userData:", this.userData);

        // עדכון ה-FormGroup עם הנתונים מהשרת
        this.userForm.patchValue({
          userName: this.userData.userName || '',
          email: this.userData.email || '',
          address: this.userData.address || '',
          phone: this.userData.phone || ''
        });
      },
      error: (err) => {
        console.log("שגיאה בהבאת המשתמש לפי id:", err);
      }
    });
  }

  saveChanges(): void {
    console.log("שליחה לפונקציית עדכון: id:", this.userId, "נתונים:", this.userForm.value);
    this._userService.updateUser(this.userId, this.userForm.value).subscribe({
      next: (res) => {
        console.log("העדכון עבר בהצלחה", res);
        this._router.navigate(['/login']);
      },
      error: (err) => {
        console.log("שגיאה בעדכון!", err);
      }
    });
  }
}
