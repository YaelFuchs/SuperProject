import { Injectable } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { PopupComponent } from '../popup/popup.component';

@Injectable({
  providedIn: 'root'
})
export class PopupService {
  private dialogRef: MatDialogRef<PopupComponent> | null = null; // שמירת ההתייחסות לדיאלוג

  constructor(private dialog: MatDialog) { }

  openPopup(title: string, content: string) {
    this.dialogRef = this.dialog.open(PopupComponent, {
      width: '400px',
      maxWidth: '90vw',
      data: { title, content }, // העברת הנתונים לדיאלוג
      panelClass: 'custom-dialog',
      position: { top: '50vh', left: '50vw' },
      disableClose: false
    });

    // אופציונלי: טיפול בסגירה ידנית של הדיאלוג
    this.dialogRef.afterClosed().subscribe(() => {
      this.dialogRef = null; // ניקוי ההתייחסות לאחר סגירה
    });
  }

  closePopup() {
    if (this.dialogRef) {
      this.dialogRef.close();
      this.dialogRef = null; // ניקוי ההתייחסות
    }
  }
}