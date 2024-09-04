import {inject, Injectable, signal} from '@angular/core';
import {HttpClient, HttpResponse} from "@angular/common/http";
import {map, Observable, tap} from "rxjs";
import {BookResponseDTO} from "../interfaces/book-response.dto";
import {Pagination} from "../helpers/pagination";
import {PagedList} from "../helpers/paged-list";
import {BookDetailsResponseDTO} from "../interfaces/book-details-response.dto";
import {BookCreateRequestDTO} from "../interfaces/book-request.dto";

@Injectable({
  providedIn: 'root'
})
export class BookService {
  private baseBookUrl = 'https://localhost:7295/api/book';
  private baseLoanUrl = 'https://localhost:7295/api/loan';

  private http = inject(HttpClient);

  pagedList = signal<PagedList<BookResponseDTO>>({
    items: [],
    pagination: {
      TotalCount: 0,
      PageSize: 6,
      CurrentPage: 1,
      TotalPages: 1,
      HasNext: false,
      HasPrevious: false
    }
  })

  getBooks(params: Record<string, any>): Observable<PagedList<BookResponseDTO>> {
    return this.http.post<BookResponseDTO[]>(`${this.baseBookUrl}/get-all`, params, { observe: 'response' })
      .pipe(
        map((res: HttpResponse<BookResponseDTO[]>) => {
          const paginationHeader = res.headers.get('x-pagination');
          let pagination: Pagination;

          if (paginationHeader) {
            pagination = JSON.parse(paginationHeader);
          } else {
            pagination = {
              TotalCount: 0,
              PageSize: 1,
              CurrentPage: 1,
              TotalPages: 1,
              HasNext: false,
              HasPrevious: false
            };
          }

          this.pagedList.set({
            items: res.body ?? [],
            pagination: pagination
          });

          return {
            items: res.body ?? [],
            pagination: pagination
          } as PagedList<BookResponseDTO>;
        })
      );
  }

  getBookById(id: string): Observable<BookDetailsResponseDTO> {
    return this.http.get<BookDetailsResponseDTO>(`${this.baseBookUrl}/get-by-id/${id}`);
  }

  createBook(bookData: FormData): Observable<void> {
    return this.http.post<void>(`${this.baseBookUrl}/create`, bookData);
  }

  updateBook(id: string, formData: FormData): Observable<any> {
    return this.http.put(`${this.baseBookUrl}/update/${id}`, formData);
  }

  deleteBook(bookId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseBookUrl}/delete/${bookId}`);
  }
}
