import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PopupComponent } from '../popup/popup.component';

@Injectable({
  providedIn: 'root'
})
export class PopupService {
  constructor(private dialog: MatDialog) { }

  openPopup(title: string, content: string) {
    this.dialog.open(PopupComponent, {
      width: '400px', // רוחב קבוע
      maxWidth: '90vw', // מגביל את הרוחב במסכים קטנים
      data: { title, content },
      panelClass: 'custom-dialog', // נוסיף מחלקה לעיצוב
      position: { top: '50vh', left: '50vw' }, // מיקום התחלתי במרכז
      disableClose: false // מאפשר סגירה בלחיצה מחוץ לדיאלוג
    });
  }
}