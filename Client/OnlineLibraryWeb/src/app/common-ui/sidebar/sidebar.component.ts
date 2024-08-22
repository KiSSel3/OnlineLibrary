import {Component, inject, OnDestroy, OnInit} from '@angular/core';
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatSelectModule } from "@angular/material/select";
import {MatInput} from "@angular/material/input";
import {BookService} from "../../services/book.service";
import {BookResponseDTO} from "../../interfaces/book-response.dto";
import {AuthorService} from "../../services/author.service";
import {AuthorResponseDTO} from "../../interfaces/author.response.dto";
import {NgForOf} from "@angular/common";
import {GenreService} from "../../services/genre.service";
import {GenreDTO} from "../../interfaces/genre.dto";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {debounceTime, map, startWith, Subscription, switchMap} from "rxjs";
import {FormStateService} from "../../services/form-state.service";
import {takeUntilDestroyed} from "@angular/core/rxjs-interop";

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    MatInput,
    NgForOf,
    ReactiveFormsModule
  ],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  bookService = inject(BookService);
  authorService = inject(AuthorService);
  genreService = inject(GenreService);
  formStateService = inject(FormStateService);

  authors: AuthorResponseDTO[] = [];
  genres: GenreDTO[] = [];

  form = new FormGroup({
    searchName: new FormControl(null),
    genreId: new FormControl(null),
    authorId: new FormControl(null),
  })

  formSub! : Subscription;

  constructor() {
    this.authorService.getAllAuthors().subscribe(data => {
      this.authors = data;
    });

    this.genreService.getAllGenres().subscribe(data => {
      this.genres = data;
    })

    this.form.valueChanges
      .pipe(
        startWith({
          pageNumber: 1,
          pageSize: 6
        }),
        debounceTime(500),
        map(formValue => ({
          ...formValue,
          pageNumber: 1,
          pageSize: 6
        })),
        switchMap(formValue => {
          this.formStateService.updateFormState(this.form);

          return this.bookService.getBooks(formValue);
        }),
        takeUntilDestroyed()
      )
      .subscribe()
  }
}
