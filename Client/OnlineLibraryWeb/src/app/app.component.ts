import {Component, inject} from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {LoginPageComponent} from "./pages/login-page/login-page.component";
import { MatDialogModule } from '@angular/material/dialog';
import {RegisterPageComponent} from "./pages/register-page/register-page.component";
import {BookCardComponent} from "./common-ui/book-card/book-card.component";
import {JsonPipe, NgForOf} from "@angular/common";
import {SearchPageComponent} from "./pages/search-page/search-page.component";
import {BookDetailsPageComponent} from "./pages/book-details-page/book-details-page.component";
import {SidebarComponent} from "./common-ui/sidebar/sidebar.component";
import {ProfilePageComponent} from "./pages/profile-page/profile-page.component";



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MatDialogModule,
    LoginPageComponent,
    RegisterPageComponent,
    BookCardComponent,
    SearchPageComponent,
    BookDetailsPageComponent,
    SidebarComponent,
    ProfilePageComponent,
    LoginPageComponent,
    JsonPipe,
    NgForOf
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'OnlineLibraryWeb';
}
