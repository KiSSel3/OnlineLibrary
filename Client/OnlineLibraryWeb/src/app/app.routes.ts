import { Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {LoginPageComponent} from "./pages/login-page/login-page.component";
import {RegisterPageComponent} from "./pages/register-page/register-page.component";
import {BookCardComponent} from "./common-ui/book-card/book-card.component";
import {SearchPageComponent} from "./pages/search-page/search-page.component";
import {canActivateAuth} from "./guards/access.guard";

export const routes: Routes = [
  { path: '', component: SearchPageComponent, canActivate: [canActivateAuth] },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
];
