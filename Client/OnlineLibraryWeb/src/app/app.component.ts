import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {LoginPageComponent} from "./pages/login-page/login-page.component";
import { MatDialogModule } from '@angular/material/dialog';
import {RegisterPageComponent} from "./pages/register-page/register-page.component";



@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoginPageComponent, RegisterPageComponent, MatDialogModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'OnlineLibraryWeb';
}
