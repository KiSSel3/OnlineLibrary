import {Component, inject, OnInit} from '@angular/core';
import {MatDialogActions, MatDialogContent, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {MatButton, MatButtonModule} from "@angular/material/button";
import {MatFormField, MatFormFieldModule, MatLabel} from "@angular/material/form-field";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {CommonModule, NgForOf, NgIf} from "@angular/common";
import {MatInput, MatInputModule} from "@angular/material/input";
import {BookService} from "../../services/book.service";
import {GenreService} from "../../services/genre.service";
import {AuthorService} from "../../services/author.service";
import {AuthorResponseDTO} from "../../interfaces/author.response.dto";
import {GenreDTO} from "../../interfaces/genre.dto";
import {MatOption} from "@angular/material/core";
import {MatSelect, MatSelectModule} from "@angular/material/select";
import {BookCreateRequestDTO} from "../../interfaces/book-request.dto";

@Component({
  selector: 'app-book-create-dialog',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatDialogModule,
    ReactiveFormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './book-create-dialog.component.html'
})
export class BookCreateDialogComponent implements OnInit {
  readonly dialogRef = inject(MatDialogRef<BookCreateDialogComponent>);
  readonly fb = inject(FormBuilder);
  readonly bookService = inject(BookService);
  readonly genreService = inject(GenreService);
  readonly authorService = inject(AuthorService);


  bookForm: FormGroup = this.fb.group({
    isbn: ['', Validators.required],
    title: ['', Validators.required],
    description: ['', Validators.required],
    genreId: [null, Validators.required],
    authorId: [null, Validators.required]
  });

  authors: AuthorResponseDTO[] = [];
  genres: GenreDTO[] = [];
  selectedFile: File | null = null;
  imageError: string | null = null;

  ngOnInit() {
    this.genreService.getAllGenres().subscribe(genres => this.genres = genres);
    console.log(this.genres);

    this.authorService.getAllAuthors().subscribe(authors => this.authors = authors);
    console.log(this.authors);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file && file.type.startsWith('image/')) {
      this.selectedFile = file;
      this.imageError = null;
    } else {
      this.imageError = 'Invalid file type. Please select an image file.';
    }
  }

  onClick() {
    if (this.bookForm.valid) {
      const formData = new FormData();
      formData.append('ISBN', this.bookForm.get('isbn')?.value);
      formData.append('Title', this.bookForm.get('title')?.value);
      formData.append('Description', this.bookForm.get('description')?.value);

      if (this.selectedFile) {
        formData.append('Image', this.selectedFile);
      }

      formData.append('GenreId', this.bookForm.get('genreId')?.value);
      formData.append('AuthorId', this.bookForm.get('authorId')?.value);

      this.bookService.createBook(formData).subscribe({
        next: () => {
          this.dialogRef.close();
        },
        error: (error) => {
          console.error('Failed to create book', error);
        }
      });
    }
  }

}
