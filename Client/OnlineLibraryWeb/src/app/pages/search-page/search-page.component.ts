import {Component, inject, OnInit} from '@angular/core';
import {BookCardComponent} from "../../common-ui/book-card/book-card.component";
import {JsonPipe, NgForOf} from "@angular/common";
import {BookService} from "../../services/book.service";
import {SidebarComponent} from "../../common-ui/sidebar/sidebar.component";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {FormStateService} from "../../services/form-state.service";
import {switchMap, tap} from "rxjs";

@Component({
  selector: 'app-search-page',
  standalone: true,
  imports: [
    BookCardComponent,
    NgForOf,
    SidebarComponent,
    MatPaginator,
    JsonPipe
  ],
  templateUrl: './search-page.component.html',
  styleUrl: './search-page.component.css'
})
export class SearchPageComponent {
  formService = inject(FormStateService);
  bookService = inject(BookService);
  pagedList = this.bookService.pagedList;

  form$ = this.formService.formState$;

  handlePageEvent(e: PageEvent) {
    const pageSize = e.pageSize;
    const currentPage = e.pageIndex + 1;

    this.form$.pipe(
      switchMap(form => {
        const formValue = form.value;
        const params = {
          ...formValue,
          pageNumber: e.pageIndex + 1,
          pageSize: e.pageSize
        };

        return this.bookService.getBooks(params);
      }),
      tap(pagedList => {
        this.pagedList.set(pagedList);
      })
    ).subscribe()
  }
}
