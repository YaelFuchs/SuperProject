import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../auth/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  constructor(private _authService: AuthService) { }

  logout() {
    this._authService.logout()
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isManager(): boolean {
    return this._authService.isManager();
  }
  isUser():boolean{
    return this._authService.isUser();
  }
}

