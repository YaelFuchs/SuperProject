import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-not-found',
  standalone: true,
  imports: [], // לא נדרש ייבוא נוסף כרגע
  templateUrl: './not-found.component.html',
  styleUrl: './not-found.component.scss'
})
export class NotFoundComponent {
  constructor(private router: Router) {}

  goBack() {
    this.router.navigate(['product/']); 
  }
}