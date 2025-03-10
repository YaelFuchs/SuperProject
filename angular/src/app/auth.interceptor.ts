
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