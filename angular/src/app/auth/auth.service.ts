import { HttpClient } from "@angular/common/http";
import { Injectable, PLATFORM_ID, Inject, OnInit } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Auth } from "./auth.model";
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',

})
export class AuthService implements OnInit {
  basicUrl = 'https://localhost:7173/api/Auth';
  $source: Observable<number> = new Observable<number>((observe) => {
    observe.next(1);
    observe.complete();
    observe.error('error');
  });

  isAuthenticated$ = new BehaviorSubject<boolean>(false); // השם הנכון
  private roles: string[] = [];
  public userId: number = 0;


  constructor(
    private _httpClient: HttpClient, private _router: Router,
    @Inject(PLATFORM_ID) private _platformId: Object) { }

  ngOnInit(): void {
    if (isPlatformBrowser(this._platformId)) {
      this.checkAuth();
    }

  }
  private checkAuth(): void {
    const tokenString = localStorage.getItem('authToken');
    if (tokenString) {
      try {
        const tokenObj = JSON.parse(tokenString);
        const token = tokenObj.token;
        this.isAuthenticated$.next(true);
        this.roles = this.getRolesFromToken(token);
      } catch (error) {
        console.error('שגיאה בפרסור הטוקן:', error);
        this.isAuthenticated$.next(false);
        this.roles = [];
      }
    } else {
      this.isAuthenticated$.next(false);
      this.roles = [];
    }
  }

  private getRolesFromToken(token: string): string[] {
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const roleClaim = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';
      const roles = payload[roleClaim];
      console.log("תפקידים: ", roles);

      return Array.isArray(roles) ? roles : [roles];
    } catch (e) {
      console.error('שגיאה בפענוח הטוקן:', e);
      return [];
    }
  }

  isAdmin(): boolean {
    this.checkAuth();
    return this.roles.includes('ROLE_USER,ROLE_ADMIN,ROLE_MANAGER') ||
      this.roles.includes('ROLE_USER,ROLE_ADMIN');
  }
  isManager(): boolean {
    this.checkAuth();
    return this.roles.includes('ROLE_USER,ROLE_ADMIN,ROLE_MANAGER');
  }
  isUser(): boolean {
    this.checkAuth();
    return this.roles.includes('ROLE_USER');
  }
  get isAuthenticated(): boolean {
    return this.isAuthenticated$.getValue();
  }

  login(auth: Auth): void {
    this._httpClient.post<{ id: number, token: string }>(this.basicUrl, auth, { withCredentials: true }).subscribe({
      next: (res) => {
        console.log('תגובת השרת:', res);
        if (isPlatformBrowser(this._platformId)) {
          try {
            localStorage.setItem('authToken', JSON.stringify({ userId: res.id, token: res.token }));
            console.log('✅ טוקן נשמר בהצלחה:', localStorage.getItem('authToken'));
            this.isAuthenticated$.next(true);
            this.roles = this.getRolesFromToken(res.token);
          } catch (error) {
            console.error('שגיאה בשמירה:', error);
          }
        }
        this._router.navigate(['/product'])
      },
      error: (err) => {
        alert('שגיאה בהתחברות: ' + err.error);
      }
    });
  }

  getToken(): string | null {
    if (isPlatformBrowser(this._platformId)) {
      const tokenString = localStorage.getItem('authToken');
      return tokenString ? JSON.parse(tokenString).token : null;
    }
    return null;
  }

  logout(): void {
    this._httpClient.post(`${this.basicUrl}/logout`, {}, { withCredentials: true }).subscribe({
      next: () => {
        console.log('✅ התנתקות בוצעה בהצלחה');
        localStorage.removeItem('authToken');
        const cheak = localStorage.getItem('orderPrice');
        if (cheak) {
          localStorage.removeItem('orderPrice');
        }
        this.isAuthenticated$.next(false);
        this._router.navigate(['/login']);
      },
      error: (err) => {
        console.error('❌ שגיאה בהתנתקות:', err);
      }
    });
  }

}  