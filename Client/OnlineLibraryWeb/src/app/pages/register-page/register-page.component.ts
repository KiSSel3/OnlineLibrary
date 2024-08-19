import {Component, inject} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {AuthService} from "../../services/auth.service";
import {MatDialog} from "@angular/material/dialog";
import {Router} from "@angular/router";
import {AuthErrorDialogComponent} from "../../common-ui/auth-error-dialog/auth-error-dialog.component";

@Component({
  selector: 'app-register-page',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.css'
})
export class RegisterPageComponent {
  private readonly authService = inject(AuthService);
  private readonly dialog = inject(MatDialog)
  private readonly router = inject(Router)

  form = new FormGroup({
    login: new FormControl(null, Validators.required),
    password: new FormControl(null, Validators.required),
    email: new FormControl(null, Validators.email),
  })

  onSubmit() {
    if (this.form.valid) {
      //@ts-ignore
      this.authService.register(this.form.value).subscribe(
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
