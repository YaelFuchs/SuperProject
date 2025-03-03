// import {  HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
// import { Injectable } from "@angular/core";
// import { AuthService } from "./auth/auth.service";
// import { Observable } from "rxjs";



// @Injectable()
// export class AuthInterceptor implements HttpInterceptor {
//     constructor(private _authService: AuthService) {
//         console.log("🚀 AuthInterceptor נטען!");
//     }

//     intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//         console.log("✅ AuthInterceptor מופעל!");

//         const token = this._authService.getToken();
//         if (token) {
//             console.log("🔑 נמצא טוקן:", token);
//             const cloneReq = req.clone({
//                 setHeaders: {
//                     Authorization: `Bearer ${token}`
//                 }
//             });
//             return next.handle(cloneReq);
//         }

//         console.log("⚠️ אין טוקן - הבקשה נשלחת ללא Authorization");
//         return next.handle(req);
//     }
// }

// import { HttpInterceptorFn } from '@angular/common/http';
// import { inject } from '@angular/core';
// import { AuthService } from './auth/auth.service';


// export const authInterceptor: HttpInterceptorFn = (request, next) => {
//   const authService = inject(AuthService); // הזרקת השירות
//   const isRequestAuthorized =
//     authService.isAuthenticated$.value && request.url.startsWith('https://localhost:7173/api');

//   const tokenString = localStorage.getItem('authToken');
//   const token = tokenString ? JSON.parse(tokenString).token : null;

//   if (isRequestAuthorized && token) {
//     const clonedRequest = request.clone({
//       setHeaders: {
//         Authorization: `Bearer ${token}`
//       }
//     });
//     return next(clonedRequest);
//   }

//   return next(request);
// };


import { HttpInterceptorFn } from '@angular/common/http';
import { inject, PLATFORM_ID } from '@angular/core';
import { AuthService } from './auth/auth.service';
import { isPlatformBrowser } from '@angular/common';

export const authInterceptor: HttpInterceptorFn = (request, next) => {
  const authService = inject(AuthService);
  const platformId = inject(PLATFORM_ID);
  const isBrowser = isPlatformBrowser(platformId);

  // תיקון: שימוש ב-isAuthenticated$ במקום isAuthtnticated$
  const isRequestAuthorized =
    authService.isAuthenticated$.value && request.url.startsWith('https://localhost:7173/api');
  const token = authService.getToken();

  if (isRequestAuthorized && token && isBrowser) {
    const clonedRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
    return next(clonedRequest);
  }

  return next(request);
};