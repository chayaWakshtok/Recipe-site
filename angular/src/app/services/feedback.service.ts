import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { Feedback } from '../models/feedback';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService {

  auth_api = environment.base_url + "Feedback/";

  constructor(public http: HttpClient) { }

  getFeedbacks(recipe: number): Observable<Feedback[]> {
    return this.http.get<ServiceResponse<Feedback[]>>(this.auth_api + "GetByRecipe/" + recipe)
      .pipe(map((response: ServiceResponse<Feedback[]>) => { return response.data }));
  }

  add(cat: Feedback): Observable<Feedback[]> {
    return this.http.post<ServiceResponse<Feedback[]>>(this.auth_api + "Add", cat)
      .pipe(map((response: ServiceResponse<Feedback[]>) => { return response.data }));
  }
}
