import {Component, Inject} from '@angular/core';
import {AuthorResponseDTO} from "../../interfaces/author.response.dto";
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {GenreDTO} from "../../interfaces/genre.dto";
import {MAT_DIALOG_DATA, MatDialogModule, MatDialogRef} from "@angular/material/dialog";
import {BookService} from "../../services/book.service";
import {GenreService} from "../../services/genre.service";
import {AuthorService} from "../../services/author.service";
import {BookDetailsResponseDTO} from "../../interfaces/book-details-response.dto";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatSelectModule} from "@angular/material/select";
import {MatButtonModule} from "@angular/material/button";
import {NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-book-edit-dialog',
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
  templateUrl: './book-edit-dialog.component.html'
})
export class BookEditDialogComponent {
  bookForm: FormGroup;
  authors: AuthorResponseDTO[] = [];
  genres: GenreDTO[] = [];
  selectedFile: File | null = null;
  imageError: string | null = null;

  constructor(
    private dialogRef: MatDialogRef<BookEditDialogComponent>,
    private fb: FormBuilder,
    private bookService: BookService,
    private genreService: GenreService,
    private authorService: AuthorService,
    @Inject(MAT_DIALOG_DATA) public data: { book: BookDetailsResponseDTO }
  ) {
    this.bookForm = this.fb.group({
      isbn: [{ value: '', disabled: true }, Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      genreId: [null, Validators.required],
      authorId: [null, Validators.required]
    });
  }

  ngOnInit() {
    this.genreService.getAllGenres().subscribe(genres => this.genres = genres);
    this.authorService.getAllAuthors().subscribe(authors => this.authors = authors);

    if (this.data && this.data.book) {
      this.bookForm.patchValue({
        isbn: this.data.book.isbn,
        title: this.data.book.title,
        description: this.data.book.description,
        genreId: this.data.book.genreResponseDTO.id,
        authorId: this.data.book.authorResponseDTO.id
      });
    }
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
  onClick(): void {
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

      this.bookService.updateBook(this.data.book.id.toString(), formData).subscribe({
        next: () => this.dialogRef.close(),
        error: (error) => console.error('Failed to update book', error)
      });
    }
  }
}
