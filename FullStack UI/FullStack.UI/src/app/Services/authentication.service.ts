import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiUrl = 'https://localhost:7211/api/Auth'; // Replace with your authentication API URL
  private tokenKey = 'jwtToken';

  constructor(private http: HttpClient) {}

  login(UserName: string, Password: string) {
    this.http.post<any>(this.apiUrl, { UserName, Password }).subscribe(
      response => {
        if (response && response.token) {
          localStorage.setItem(this.tokenKey, response.token);
        }
      },
      error => {
        console.error('Error logging in:', error);
      }
    );
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
}
