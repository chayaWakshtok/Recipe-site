import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, map, of, throwError } from 'rxjs';
import { Router } from '@angular/router';

const USER_KEY = 'auth-user';

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {

  constructor(public router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const userData = JSON.parse(window.sessionStorage.getItem(USER_KEY) || '{}');
    if (userData.token) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${userData.token}`
        }
      });
    }
    return next.handle(request).pipe(
      map((res: any) => {
        if (res.type != 0) {
          if (res.body.success == false)
            throwError(res.message);
          return res;
        }
      }),
      catchError((error: HttpErrorResponse) => {
        let errorMsg = '';
        if (error.error instanceof ErrorEvent) {
          console.log('This is client side error');
          errorMsg = `Error: ${error.error.message}`;
        } else {
          console.log('This is server side error');
          errorMsg = `${error.error.message}`;
          switch (error.status) {
            case 401:      //login
              this.router.navigateByUrl("/login");
              console.log(`redirect to login`);
              // handled = true;
              break;
            case 403:     //forbidden
              this.router.navigateByUrl("/login");
              console.log(`redirect to login`);
              // handled = true;
              break;
          }
        }
        console.log(errorMsg);
        return throwError(error);

        //   let handled: boolean = false;
        //   console.error(error);
        //   if (error instanceof HttpErrorResponse) {
        //     if (error.error instanceof ErrorEvent) {
        //       console.error("Error Event");
        //     } else {
        //       console.log(`error status : ${error.status} ${error.statusText}`);
        //       switch (error.status) {
        //         case 401:      //login
        //           this.router.navigateByUrl("/login");
        //           console.log(`redirect to login`);
        //           handled = true;
        //           break;
        //         case 403:     //forbidden
        //           this.router.navigateByUrl("/login");
        //           console.log(`redirect to login`);
        //           handled = true;
        //           break;
        //       }
        //     }
        //   }
        //   else {
        //     console.error("Other Errors");
        //   }

        //   if (handled) {
        //     console.log('return back ');
        //     return of(error);
        //   } else {
        //     console.log('throw error back to to the subscriber');
        //     return throwError(error);
        //   }

      })
    )
  }
}

export const httpInterceptorProviders = [
  { provide: HTTP_INTERCEPTORS, useClass: HttpRequestInterceptor, multi: true },
];
