import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { authGuard } from './auth.guard';

export const routes: Routes = [
    { path: "", redirectTo: "home", pathMatch: "full" },
    { path: "home", component: HomeComponent },
    {path: "category", loadChildren: ()=>import ('./category/category.module').then(c=>c.CategoryModule)},
    { path: "**", loadComponent: () => import('./not-found/not-found.component').then(c => c.NotFoundComponent), canActivate: [authGuard] }
];
