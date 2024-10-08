import { Routes } from '@angular/router';
import {AppComponent} from "./app.component";
import {LoginPageComponent} from "./pages/login-page/login-page.component";
import {RegisterPageComponent} from "./pages/register-page/register-page.component";
import {BookCardComponent} from "./common-ui/book-card/book-card.component";
import {SearchPageComponent} from "./pages/search-page/search-page.component";
import {canActivateAuth} from "./guards/access.guard";
import {BookDetailsPageComponent} from "./pages/book-details-page/book-details-page.component";
import {ProfilePageComponent} from "./pages/profile-page/profile-page.component";
import {LayoutComponent} from "./common-ui/layout/layout.component";

export const routes: Routes = [
  {path: '', component: LayoutComponent, children: [
      { path: '', component: SearchPageComponent },
      { path: 'profile', component: ProfilePageComponent },
      { path: 'book/:id', component: BookDetailsPageComponent },
    ],
    canActivate: [canActivateAuth]
  },
  { path: 'login', component: LoginPageComponent },
  { path: 'register', component: RegisterPageComponent },
];
