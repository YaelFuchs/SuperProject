// import { HttpClient } from "@angular/common/http";
// import { Injectable } from "@angular/core";
// import { BehaviorSubject, Observable } from "rxjs";
// import { Auth } from "./auth.model";

// @Injectable({
//     providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
// })
// export class AuthService{
//     basicUrl='https://localhost:7173/api/Auth';
//     $source:Observable<number>=new Observable<number>((observe)=>{
//         observe.next(1);
//         observe.complete();
//         observe.error('error');
//     })
//     constructor( private _httpClient:HttpClient){}
//     isAuthtnticated$= new BehaviorSubject<boolean>(!!localStorage.getItem("authToken"));

//     get isAuthenticated():boolean{
//         return this.isAuthtnticated$.getValue();
//     }
//      login(auth:Auth):void{
//         this._httpClient.post<{token : string}>(this.basicUrl,auth).subscribe({
//             next:(res)=>{
//                 console.log(res);
//               try{
//                 localStorage.setItem('authToken',JSON.stringify({token : res.token}))
//                 this.isAuthtnticated$.next(true);
//               } catch(error){
//                   console.log("שגיאה בשמירה",error);                 
//               }
//             },
//             error: (err)=>{               
//                alert("שגיאה בהתחברות"+err.error);              
//             }
//         })
//      }

//     // login(auth:Auth):Observable<{token : string}>{
//     //     const token= this._httpClient.post<{token : string}>(this.basicUrl,auth);
//     //     localStorage.setItem('authToken',JSON.stringify(token));
//     //     return token;
//     // }
    
//     getToken(): string | null{
//         return localStorage.getItem('authToken');
//     }
//     logout(): void {
//         localStorage.removeItem('authToken')
//     }
//     // isLoggedIn() :boolean {
//     //     return !! this.getToken();
//     // }
// }

import { HttpClient } from "@angular/common/http";
import { Injectable, PLATFORM_ID, Inject, OnInit } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Auth } from "./auth.model";
import { isPlatformBrowser } from '@angular/common';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root',
  
})
export class AuthService implements OnInit{
  basicUrl = 'https://localhost:7173/api/Auth';
  $source: Observable<number> = new Observable<number>((observe) => {
    observe.next(1);
    observe.complete();
    observe.error('error');
  });

  isAuthenticated$ = new BehaviorSubject<boolean>(false); // השם הנכון
  private roles: string[] = [];

  constructor(
    private _httpClient: HttpClient,private _router: Router,
    @Inject(PLATFORM_ID) private _platformId: Object ) {  }

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
    return this.roles.includes('ROLE_USER,ROLE_ADMIN,ROLE_MANAGER') ||
            this.roles.includes('ROLE_USER,ROLE_ADMIN') ;
  }
  isManager(): boolean {
    return this.roles.includes('ROLE_USER,ROLE_ADMIN,ROLE_MANAGER');
  }

  get isAuthenticated(): boolean {
    return this.isAuthenticated$.getValue();
  }

  login(auth: Auth): void {
    this._httpClient.post<{ token: string }>(this.basicUrl, auth).subscribe({
      next: (res) => {
        console.log('תגובת השרת:', res);
        if (isPlatformBrowser(this._platformId)) {
          try {
            localStorage.setItem('authToken', JSON.stringify({ token: res.token }));
            this.isAuthenticated$.next(true);
            this.roles = this.getRolesFromToken(res.token);
          } catch (error) {
            console.error('שגיאה בשמירה:', error);
          }
        }
        this._router.navigate(['/home'])
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
    if (isPlatformBrowser(this._platformId)) {
      localStorage.removeItem('authToken');
    }
    this.isAuthenticated$.next(false);
    this.roles = [];
  }
}