// import { HttpInterceptorFn } from "@angular/common/http";
// import { inject } from "@angular/core";
// import { AuthService } from "./auth/auth.service";


// export const errorInterceptor: HttpInterceptorFn = (request, next) => {
//   const authService = inject(AuthService);

//   const isRequestAuthorized = authService.isAuthtnticated$.value && request.url.startsWith("https://localhost:7173/api");

//   if (isRequestAuthorized) {
//     const token = localStorage.getItem("authToken");
//     if (token) {
//       const cloneRequest = request.clone({
//         setHeaders: {
//           Authorization: `Bearer ${JSON.parse(token).token}`
//         }
//       });
//       return next(cloneRequest);
//     }
//   }
//   return next(request);
// };