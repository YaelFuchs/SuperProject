import { Routes } from '@angular/router';
// import { authGuard } from './auth.guard';
import { AuthComponent } from './auth/auth.component';

export const routes: Routes = [
    { path: "", redirectTo: "product", pathMatch: "full" },
    { path: "category", loadChildren: () => import('./category/category.module').then(c => c.CategoryModule) },
    { path: "user", loadChildren: () => import('./user/user.module').then(u => u.UserModule) },
    { path: "login", component: AuthComponent },
    { path: "branch", loadChildren: () => import('./branch/branch.module').then(b => b.BranchModule) },
    { path: "product", loadChildren: () => import('./product/product.module').then(p => p.ProductModule) },
    { path: "branchProduct", loadChildren: () => import('./branchProduct/branchProduct.module').then(b => b.BranchProductModule) },
    { path: "cart", loadChildren: () => import('./cart/cart.module').then(c => c.CartModule) },
    { path: "paypal/:userId/:cost", loadChildren: () => import('./paypal-button/paypal.module').then(p => p.PayPalModule) },
    { path: "**", loadComponent: () => import('./not-found/not-found.component').then(c => c.NotFoundComponent)},
];
