import { Component } from '@angular/core';
import { User } from 'src/app/models/user';
import { StorageService } from 'src/app/services/storage.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-home-users',
  templateUrl: './home-users.component.html',
  styleUrls: ['./home-users.component.scss']
})
export class HomeUsersComponent {

  user!: User;

  constructor(public userService: UserService,
    public storageService: StorageService
  ) { }

  ngOnInit(): void {
    this.userService.getUserDetail().subscribe(res => {
      this.user = res;
    })
  }

  signOut() {
    this.storageService.clean();
    this.storageService.subjectLogin.next(false);
  }

}
