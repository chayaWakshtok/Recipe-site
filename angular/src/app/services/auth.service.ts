import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  auth_api = environment.base_url + "Authenticate/";

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<{ token: string, user: User, message: string }> {
    return this.http.post<{ token: string, user: User, message: string }>(
      this.auth_api + 'Login',
      {
        username,
        password,
      });
  }

  register(user: User): Observable<any> {
    return this.http.post(
      this.auth_api + 'Register', user);
  }

  logout(): Observable<any> {
    return this.http.post(this.auth_api + 'signout', {});
  }
}
