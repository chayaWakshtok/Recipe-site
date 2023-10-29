import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  auth_api = environment.base_url + "Users/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.auth_api + "GetAllWithPicture");
  }

  add(user: User): Observable<boolean> {
    return this.http.post<boolean>(this.auth_api + "Add", user);
  }

  delete(id: number | undefined): Observable<boolean> {
    return this.http.delete<boolean>(this.auth_api + "Delete/" + id);
  }

  getUser(id: number | undefined): Observable<User> {
    return this.http.get<User>(this.auth_api + "GetUserById/" + id);
  }

  update(user:User)
  {
    return this.http.put<boolean>(this.auth_api + "Update", user);
  }
}
