import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Observable} from "rxjs";
import {LoanResponseDTO} from "../interfaces/loan-response.dto";

@Injectable({
  providedIn: 'root'
})
export class LoanService {
  private baseUrl = 'https://localhost:7295/api/loan';
  private http = inject(HttpClient);

  checkBookAvailability(bookId: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/check-availability/${bookId}`);
  }

  createNewLoan(params: Record<string, any>): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/create-loan`, params);
  }

  getLoansByUserId(userId: string): Observable<LoanResponseDTO[]> {
    return this.http.get<LoanResponseDTO[]>(`${this.baseUrl}/get-user-loans/${userId}`);
  }

}
