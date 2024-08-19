import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TokenResponse} from "../interfaces/token-response.dto";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7295/api/user';
  private http = inject(HttpClient);

  login(loginRequest: { login: string, password: string }): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.baseUrl}/login`, loginRequest);
  }

  register(registerRequest: { login: string, password: string, email: string }): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.baseUrl}/register`, registerRequest);
  }

  logout(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/logout/${userId}`);
  }

  refreshToken(refreshToken: string): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/refresh-token`, { refreshToken });
  }
}
