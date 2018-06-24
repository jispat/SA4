import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router'; 

import { LoginComponent } from './Components/anonymous-users/login/login.component';
import { HomeComponent } from './Components/anonymous-users/home/home.component';
import { ForgotpasswordComponent } from './Components/anonymous-users/forgotpassword/forgotpassword.component';
import { ResetpasswordComponent } from './Components/anonymous-users/resetpassword/resetpassword.component';
import { PageNotFoundComponent } from './Components/shared/page-not-found/page-not-found.component';
import { AppUsersRoutes } from './Components/app-users/app-users.routes';

export const routes: Routes = [
    { path: '', redirectTo: "login", pathMatch: 'full' },
    ...AppUsersRoutes,
    //{ path: 'home', component: HomeComponent },
    { path: "login", component: LoginComponent },
    { path: 'forgotpassword', component: ForgotpasswordComponent },
    { path: 'resetpassword/:code', component: ResetpasswordComponent },
    { path: '**', component: PageNotFoundComponent }
];
// export const routes1: Routes = [
// ];

export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes);
//export const AppRoutes: ModuleWithProviders = RouterModule.forRoot(routes1);