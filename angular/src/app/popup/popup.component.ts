import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button'; // הוסף את זה

@Component({
  selector: 'app-popup',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule], // הוסף את MatButtonModule
  templateUrl: './popup.component.html',
  styleUrls: ['./popup.component.scss']
})
export class PopupComponent {
  constructor(
    public dialogRef: MatDialogRef<PopupComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { title: string; content: string }
  ) { }

  closePopup() {
    this.dialogRef.close();
  }
}