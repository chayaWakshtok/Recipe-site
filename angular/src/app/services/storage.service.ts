import { Injectable } from '@angular/core';
import { ROLE } from '../models/role';

const USER_KEY = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  constructor() { }

  clean(): void {
    window.sessionStorage.clear();
  }

  public saveUser(user: any): void {
    window.sessionStorage.removeItem(USER_KEY);
    window.sessionStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public getUser(): any {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  public IsAdmin() {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      var userData = JSON.parse(user);
      if (userData.user.roleId == ROLE.Admin)
        return true;
      return false;
    }
    return false;
  }

  public isLoggedIn(): boolean {
    const user = window.sessionStorage.getItem(USER_KEY);
    if (user) {
      return true;
    }

    return false;
  }
}
