import { Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {LoginPageComponent} from "./pages/login-page/login-page.component";
import {RegisterPageComponent} from "./pages/register-page/register-page.component";

export const routes: Routes = [
  { path: '', component: AppComponent },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
];
