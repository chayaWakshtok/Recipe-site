import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ROLE, Role } from 'src/app/models/role';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-login-admin',
  templateUrl: './login-admin.component.html',
  styleUrls: ['./login-admin.component.scss'],
})
export class LoginAdminComponent implements OnInit {

  username: string = "";
  password: string = "";
  captcha: string = "";
  captchaCode: string = "";
  errorLogin = "";

  constructor(public authService: AuthService,
    public storageService: StorageService,
    public router: Router) {

  }

  ngOnInit(): void {
    this.captchaCode = (Math.random()).toString(36).substr(2, 8);
  }

  onSubmit() {
    console.log(JSON.stringify({ user: this.username, password: this.password, captch: this.captcha }));
    this.authService.login(this.username, this.password).subscribe(res => {
      debugger;
      if (res.user.roleId != ROLE.Admin)
      {
        this.errorLogin = "INCORRECT LOGIN DATA OR ACCESS DENIED";
        this.captchaCode = (Math.random()).toString(36).substr(2, 8);
      }
      else {
        this.storageService.saveUser(res);
        this.router.navigate(["admin"]);
      }
    }, ex => {
      this.errorLogin = "INCORRECT LOGIN DATA OR ACCESS DENIED";
    })

  }


}
