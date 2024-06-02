import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  auth_api = environment.base_url + "Users/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this.http.get<ServiceResponse<User[]>>(this.auth_api + "GetAllWithPicture")
      .pipe(map((response: ServiceResponse<User[]>) => { return response.data }));
  }

  add(user: User): Observable<User[]> {
    return this.http.post<ServiceResponse<User[]>>(this.auth_api + "Add", user)
      .pipe(map((response: ServiceResponse<User[]>) => { return response.data }));
  }

  delete(id: number | undefined): Observable<User[]> {
    return this.http.delete<ServiceResponse<User[]>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<User[]>) => { return response.data }));
  }

  getUser(id: number | undefined): Observable<User> {
    return this.http.get<ServiceResponse<User>>(this.auth_api + "GetUserById/" + id)
      .pipe(map((response: ServiceResponse<User>) => { return response.data; }));
  }

  getUserDetail(): Observable<User> {
    return this.http.get<ServiceResponse<User>>(this.auth_api + "GetUserDetails")
      .pipe(map((response: ServiceResponse<User>) => { return response.data; }));
  }

  update(user: User) {
    return this.http.put<ServiceResponse<User[]>>(this.auth_api + "Update", user)
      .pipe(map((response: ServiceResponse<User[]>) => { return response.data; }));
  }
}
