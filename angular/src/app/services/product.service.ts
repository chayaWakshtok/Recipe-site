import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ServiceResponse } from '../models/service-response';
import { Product } from '../models/product';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  auth_api = environment.base_url + "Product/";

  constructor(private http: HttpClient) { }

  search(term: any):any {
    if (term === '') {
      return of([]);
    }

    return this.http
      .get<ServiceResponse<Product[]>>(this.auth_api + "Search/" + term)
      .pipe(map((response) => response.data));
  }

  getAll(): Observable<Product[]> {
    return this.http.get<ServiceResponse<Product[]>>(this.auth_api + "GetAll")
      .pipe(map((response: ServiceResponse<Product[]>) => { return response.data }));
  }

  add(cat: Product): Observable<Product[]> {
    return this.http.post<ServiceResponse<Product[]>>(this.auth_api + "Add", cat)
      .pipe(map((response: ServiceResponse<Product[]>) => { return response.data }));
  }

  delete(id: number | undefined): Observable<Product[]> {
    return this.http.delete<ServiceResponse<Product[]>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<Product[]>) => { return response.data }));
  }

  getCat(id: number | undefined): Observable<Product> {
    return this.http.get<ServiceResponse<Product>>(this.auth_api + id)
      .pipe(map((response: ServiceResponse<Product>) => { return response.data; }));
  }

  update(cat: Product) {
    return this.http.put<ServiceResponse<Product[]>>(this.auth_api + "Update", cat)
      .pipe(map((response: ServiceResponse<Product[]>) => { return response.data; }));
  }
}
