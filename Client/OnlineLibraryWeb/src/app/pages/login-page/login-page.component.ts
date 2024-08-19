import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import { MatDialog } from '@angular/material/dialog';
import {AuthErrorDialogComponent} from "../../common-ui/auth-error-dialog/auth-error-dialog.component";
import {Router} from "@angular/router";
import {MatFormField} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {MatButton} from "@angular/material/button";

@Component({
  selector: 'app-login-page',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatInput,
    MatButton
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.css'
})
export class LoginPageComponent {
  private readonly authService = inject(AuthService);
  private readonly dialog = inject(MatDialog)
  private readonly router = inject(Router)

  form = new FormGroup({
    login: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required)
  })

  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.authService.login(this.form.value).subscribe(
        (response) => {
          localStorage.setItem('accessToken', response.accessToken);
          localStorage.setItem('refreshToken', response.refreshToken);

          this.router.navigate(['']);
        },
        (error) => {
          const errorMessage = error.error?.error || 'An unknown error occurred.';
          this.openDialog(errorMessage);
        }
      );
    } else {
      this.openDialog(`Form is not valid. Please correct the errors.`);
    }
  }

  private openDialog(errorMessage: string) {
    this.dialog.open(AuthErrorDialogComponent, {
      data: {message: errorMessage}
    });
  }
}
