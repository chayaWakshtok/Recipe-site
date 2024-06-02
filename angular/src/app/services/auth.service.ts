import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  auth_api = environment.base_url + "Authenticate/";

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<User> {
    return this.http.post<ServiceResponse<User>>(
      this.auth_api + 'Login',
      {
        username,
        password,
      }).pipe(map((res: ServiceResponse<User>) => {
        return res.data }));
  }

  register(user: User): Observable<ServiceResponse<number>> {
    return this.http.post<ServiceResponse<number>>(
      this.auth_api + 'Register', user);
  }

}
