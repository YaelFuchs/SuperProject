import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private _router: Router) { }

  goToCategories() {
    this._router.navigate(['/category'])
  }
  goToUser() {
    this._router.navigate(['/user'])
  }
  goToLogin() {
    this._router.navigate(['/login'])
  }
  goToBranch() {
    this._router.navigate(['/branch'])
  }
  goToProduct() {
    this._router.navigate(['/product'])
  }
  goToBranchProduct() {
    this._router.navigate(['/branchProduct'])
  }
}
//====================================================
//לבדוק מה העניין בדף הזה
