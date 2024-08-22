import {Component, inject} from '@angular/core';
import {MatDialogActions, MatDialogContent, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {MatButton} from "@angular/material/button";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {GenreService} from "../../services/genre.service";
import {AuthorService} from "../../services/author.service";
import {MatInput, MatInputModule} from "@angular/material/input";
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from "@angular/material/datepicker";
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {NgIf} from "@angular/common";
import {MatNativeDateModule} from "@angular/material/core";

@Component({
  selector: 'app-author-create-dialog',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatDialogModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    NgIf,
    MatButton
  ],
  templateUrl: './author-create-dialog.component.html'
})
export class AuthorCreateDialogComponent {
  readonly dialogRef = inject(MatDialogRef<AuthorCreateDialogComponent>);
  readonly authorService = inject(AuthorService);
  readonly fb = inject(FormBuilder);

  authorForm: FormGroup = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
    country: ['', Validators.required]
  });

  onNoClick(): void {
    this.dialogRef.close();
  }

  onClick(): void {
    if (this.authorForm.valid) {
      const authorParams: Record<string, any> = this.authorForm.value;
      this.authorService.createAuthor(authorParams).subscribe({
        next: () => {
          this.dialogRef.close();
        },
        error: (error) => {
          console.error('Error creating author', error);
        }
      });
    }
  }
}
