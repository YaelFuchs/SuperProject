import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private _router: Router, private _authService: AuthService) { }

  logout(){
   this._authService.logout()

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

