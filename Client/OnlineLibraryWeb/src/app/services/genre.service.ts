import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {GenreDTO} from "../interfaces/genre.dto";

@Injectable({
  providedIn: 'root'
})
export class GenreService {
  private baseUrl = 'https://localhost:7295/api/genre';
  private http = inject(HttpClient);

  getAllGenres(): Observable<GenreDTO[]> {
    return this.http.get<GenreDTO[]>(`${this.baseUrl}/get-all`);
  }

  getGenreById(id: string): Observable<GenreDTO> {
    return this.http.get<GenreDTO>(`${this.baseUrl}/get-by-id/${id}`);
  }
}
