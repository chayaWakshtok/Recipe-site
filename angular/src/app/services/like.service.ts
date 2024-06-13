import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Like } from '../models/like';
import { HttpClient } from '@angular/common/http';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class LikeService {
  auth_api = environment.base_url + "Like/";

  constructor(public http: HttpClient) { }

  getLikesByUser(): Observable<Like[]> {
    return this.http.get<ServiceResponse<Like[]>>(this.auth_api + "GetLikesByUser")
      .pipe(map((response: ServiceResponse<Like[]>) => { return response.data }));
  }

  delete(id:number|undefined): Observable<boolean> {
    return this.http.delete<ServiceResponse<boolean>>(this.auth_api + "Delete/" + id)
      .pipe(map((response: ServiceResponse<boolean>) => { return response.data }));
  }

  addLike(like: Like) {
    return this.http.post<ServiceResponse<Like[]>>(this.auth_api + "Add", like)
    .pipe(map((response: ServiceResponse<Like[]>) => { return response.data }));
  }
}
