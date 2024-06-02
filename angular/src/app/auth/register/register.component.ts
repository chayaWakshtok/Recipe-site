import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {

  user: User = new User();
  error: string = "";

  constructor(public router: Router,
    public authService: AuthService,
  ) {

  }
  ngOnInit(): void {
    this.user = new User();
    this.error = "";
  }
  enter() {
    this.user.roleId = 2;
    this.user.status = 1;
    this.user.firstName = this.user.username;
    this.authService.register(this.user).subscribe(res => {
      debugger;
      if (!res.success)
        this.error = res.message;
      else {
        this.router.navigate(['signin'])
      }
    }, ex => {
      debugger;
      this.error = ex.error.message;
    })

    this.error = "";
  }
}
