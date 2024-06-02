import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from 'src/app/models/user';
import { StorageService } from 'src/app/services/storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss'],
})
export class HeaderComponent implements OnInit {

  isLogin: boolean = false;
  user: User = new User();
  constructor(public router: Router, public storageService: StorageService,
    public userService: UserService
  ) { }

  ngOnInit(): void {
    this.isLogin = this.storageService.isLoggedIn();
    if (this.isLogin) {
      this.userService.getUserDetail().subscribe(data => {
        this.user = data;
      })
    }
    this.storageService.subjectLogin.subscribe((res) => {
      this.isLogin = res;
      this.userService.getUserDetail().subscribe(data => {
        this.user = data;
      })
    })
  }

  goTo() {
    this.router.navigate(['auth/signin']);
  }
}
