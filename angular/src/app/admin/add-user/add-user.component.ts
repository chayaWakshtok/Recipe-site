import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';


@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.scss']
})
export class AddUserComponent {

  user: User = new User();

  constructor(private userService: UserService,
    private router: Router,
    private toastr: ToastrService) {

  }

  saveUser() {
    this.userService.add(this.user).subscribe(res => {
      if (res.length > 0)
        this.router.navigate(["admin/users"]);
      else {
        this.toastr.error('Fail to save new user', 'Fail save!');

      }
    }, err => {
      this.toastr.error('Fail to save new user', 'Fail save!');
    })
  }

}
