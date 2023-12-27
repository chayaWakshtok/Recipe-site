import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Difficulty } from '../models/difficulty';
import { ServiceResponse } from '../models/service-response';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DifficultyService {

  auth_api = environment.base_url + "Difficulty/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<Difficulty[]> {
    return this.http.get<ServiceResponse<Difficulty[]>>(this.auth_api + "GetAll")
      .pipe(map((response: ServiceResponse<Difficulty[]>) => { return response.data }));
  }

  add(cat: Difficulty): Observable<Difficulty[]> {
    return this.http.post<ServiceResponse<Difficulty[]>>(this.auth_api + "Add", cat)
      .pipe(map((response: ServiceResponse<Difficulty[]>) => { return response.data }));
  }

  delete(id: number | undefined): Observable<Difficulty[]> {
    return this.http.delete<ServiceResponse<Difficulty[]>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<Difficulty[]>) => { return response.data }));
  }

  getDifficulty(id: number | undefined): Observable<Difficulty> {
    return this.http.get<ServiceResponse<Difficulty>>(this.auth_api + id)
      .pipe(map((response: ServiceResponse<Difficulty>) => { return response.data; }));
  }

  update(cat: Difficulty) {
    return this.http.put<ServiceResponse<Difficulty[]>>(this.auth_api + "Update", cat)
      .pipe(map((response: ServiceResponse<Difficulty[]>) => { return response.data; }));
  }
}
