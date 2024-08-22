import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AuthorResponseDTO} from "../interfaces/author.response.dto";
import {Observable} from "rxjs";
import {AuthorDTO} from "../interfaces/author.dto";

@Injectable({
  providedIn: 'root'
})
export class AuthorService {
  private baseUrl = 'https://localhost:7295/api/author';
  private http = inject(HttpClient);

  createAuthor(params: Record<string, any>): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/create`, params);
  }

  getAllAuthors(): Observable<AuthorResponseDTO[]> {
    return this.http.get<AuthorResponseDTO[]>(`${this.baseUrl}/get-all`);
  }

  getAuthorById(id: string): Observable<AuthorResponseDTO> {
    return this.http.get<AuthorResponseDTO>(`${this.baseUrl}/get-by-id/${id}`);
  }
}
