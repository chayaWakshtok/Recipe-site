import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Follow } from '../models/follow';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { ServiceResponse } from '../models/service-response';

@Injectable({
  providedIn: 'root'
})
export class FollowService {

  auth_api = environment.base_url + "Follow/";

  constructor(public http: HttpClient) { }

  getFromUser(): Observable<Follow[]> {
    return this.http.get<ServiceResponse<Follow[]>>(this.auth_api + "GetFromUser")
      .pipe(map((response: ServiceResponse<Follow[]>) => { return response.data }));
  }

  getToUser(): Observable<Follow[]> {
    return this.http.get<ServiceResponse<Follow[]>>(this.auth_api + "GetToUser")
      .pipe(map((response: ServiceResponse<Follow[]>) => { return response.data }));
  }

}
