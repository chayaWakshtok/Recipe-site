import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment.development';
import { Recipe } from '../models/recipe';
import { Observable, map } from 'rxjs';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class RecipeService {

  auth_api = environment.base_url + "Recipe/";

  constructor(private http: HttpClient) { }

  getAll(): Observable<Recipe[]> {
    return this.http.get<ServiceResponse<Recipe[]>>(this.auth_api + "GetAll")
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  getCount() {
    return this.http.get<ServiceResponse<number>>(this.auth_api + "GetCount")
      .pipe(map((response: ServiceResponse<number>) => { return response.data }));
  }

  getLaters(): Observable<Recipe[]> {
    return this.http.get<ServiceResponse<Recipe[]>>(this.auth_api + "GetLaster")
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  mostLike(): Observable<Recipe[]> {
    return this.http.get<ServiceResponse<Recipe[]>>(this.auth_api + "MostLike")
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  add(cat: Recipe): Observable<Recipe[]> {
    return this.http.post<ServiceResponse<Recipe[]>>(this.auth_api + "Add", cat)
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  delete(id: number | undefined): Observable<Recipe[]> {
    return this.http.delete<ServiceResponse<Recipe[]>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  getRecipe(id: number | undefined): Observable<Recipe> {
    return this.http.get<ServiceResponse<Recipe>>(this.auth_api + id)
      .pipe(map((response: ServiceResponse<Recipe>) => { return response.data; }));
  }

  update(cat: Recipe) {
    return this.http.put<ServiceResponse<Recipe[]>>(this.auth_api + "Update", cat)
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data; }));
  }

  getRecipesLikes(): Observable<Recipe[]> {
    return this.http.get<ServiceResponse<Recipe[]>>(this.auth_api + "GetRecipesLikes")
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }

  getMyRecipes(): Observable<Recipe[]> {
    return this.http.get<ServiceResponse<Recipe[]>>(this.auth_api + "GetMyRecipes")
      .pipe(map((response: ServiceResponse<Recipe[]>) => { return response.data }));
  }
}
