import {inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {TokenResponse} from "../interfaces/token-response.dto";
import {Observable, tap} from "rxjs";
import {CookieService} from "ngx-cookie-service";
import {jwtDecode} from "jwt-decode";
import {UserResponseDTO} from "../interfaces/user-response.dto";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private baseUrl = 'https://localhost:7295/api/user';
  private http = inject(HttpClient);
  private cookieService = inject(CookieService);

  accessToken: string | null = null;
  refreshToken: string | null = null;

  get isAuth() {
    if (!this.accessToken) {
      this.accessToken = this.cookieService.get('accessToken');
      this.refreshToken = this.cookieService.get('refreshToken');
    }

    return !!this.accessToken;
  }

  login(loginRequest: { login: string, password: string }): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.baseUrl}/login`, loginRequest)
      .pipe(
        tap(val=> {
          this.accessToken = val.accessToken
          this.refreshToken = val.refreshToken

          this.cookieService.set("accessToken", val.accessToken)
          this.cookieService.set("refreshToken", val.refreshToken)
        })
      );
  }

  register(registerRequest: { login: string, password: string, email: string }): Observable<TokenResponse> {
    return this.http.post<TokenResponse>(`${this.baseUrl}/register`, registerRequest);
  }

  logout(userId: string): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/logout/${userId}`);
  }

  refreshTokenFn(refreshToken: string): Observable<string> {
    return this.http.post<string>(`${this.baseUrl}/refresh-token`, { refreshToken });
  }

  getUserIdFromToken(): string | null {
    if (this.accessToken) {
      const decodedToken: any = jwtDecode(this.accessToken);
      const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      return userId || null;
    }
    return null;
  }

  getUserFromToken(): UserResponseDTO | null {
    if (this.accessToken) {
      const decodedToken: any = jwtDecode(this.accessToken);
      const userId = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];
      const login = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];

      return {id: userId, login: login};
    }
    return null;
  }

  clearCookies() {
    this.cookieService.delete('accessToken');
    this.cookieService.delete('refreshToken');
  }

  isAdmin(): boolean {
    if (this.accessToken) {
      const decodedToken: any = jwtDecode(this.accessToken);
      const roles = decodedToken["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
      return roles && roles.includes('Admin');
    }
    return false;
  }
}
