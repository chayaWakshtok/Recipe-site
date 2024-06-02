import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ROLE } from 'src/app/models/role';
import { AuthService } from 'src/app/services/auth.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {

  error:string="";
  username: string = "";
  password: string = "";

  constructor(public router: Router,
    public authService: AuthService,
    public storageService: StorageService
  ) { }

  ngOnInit(): void {
    this.error="";
    if (this.storageService.isLoggedIn() == true)
      this.router.navigate(["/home"]);
  }

  enter() {
    //"tamari", "123456789"
    this.authService.login(this.username,this.password).subscribe(res => {
      if (res.roleId != ROLE.User) {
        this.error = "Incorrect Email or Password";
      }
      else {
        this.storageService.saveUser(res);
        this.storageService.subjectLogin.next(true);
        this.router.navigate(["/home"]);
      }
    }, ex => {
      this.error = "Incorrect Email or Password";
    })
  }
}
