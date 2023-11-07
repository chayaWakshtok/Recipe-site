import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable } from 'rxjs';

const USER_KEY = 'auth-user';

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const userData = JSON.parse(window.sessionStorage.getItem(USER_KEY) || '{}');
    if (userData.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${userData.token}`
        }
      });
    }
    return next.handle(request);
  }
}

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true },
];
