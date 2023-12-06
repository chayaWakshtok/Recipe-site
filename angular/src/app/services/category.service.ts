import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../models/category';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  auth_api = environment.base_url + "Category/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<Category[]> {
    return this.http.get<ServiceResponse<Category[]>>(this.auth_api + "GetAll")
      .pipe(map((response: ServiceResponse<Category[]>) => { return response.data }));
  }

  add(cat: Category): Observable<Category[]> {
    return this.http.post<ServiceResponse<Category[]>>(this.auth_api + "Add", cat)
      .pipe(map((response: ServiceResponse<Category[]>) => { return response.data }));
  }

  delete(id: number | undefined): Observable<Category[]> {
    return this.http.delete<ServiceResponse<Category[]>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<Category[]>) => { return response.data }));
  }

  getCat(id: number | undefined): Observable<Category> {
    return this.http.get<ServiceResponse<Category>>(this.auth_api + id)
      .pipe(map((response: ServiceResponse<Category>) => { return response.data; }));
  }

  update(cat: Category) {
    return this.http.put<ServiceResponse<Category[]>>(this.auth_api + "Update", cat)
      .pipe(map((response: ServiceResponse<Category[]>) => { return response.data; }));
  }
}
