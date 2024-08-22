import {Component, inject, model} from '@angular/core';
import {MatDialogActions, MatDialogContent, MatDialogRef} from "@angular/material/dialog";
import {MatButton} from "@angular/material/button";
import {MatFormField, MatLabel} from "@angular/material/form-field";
import {MatInput} from "@angular/material/input";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {GenreService} from "../../services/genre.service";

@Component({
  selector: 'app-genre-create-dialog',
  standalone: true,
  imports: [
    MatButton,
    MatDialogActions,
    MatDialogContent,
    MatFormField,
    MatInput,
    MatLabel,
    ReactiveFormsModule,
    FormsModule
  ],
  templateUrl: './genre-create-dialog.component.html'
})
export class GenreCreateDialogComponent {
  readonly dialogRef = inject(MatDialogRef<GenreCreateDialogComponent>);
  readonly genreService = inject(GenreService);

  genreName: string = "";

  onNoClick(): void {
    this.dialogRef.close();
  }

  onClick(): void {
    this.genreService.createGenre(this.genreName).subscribe()
    this.dialogRef.close();
  }
}
