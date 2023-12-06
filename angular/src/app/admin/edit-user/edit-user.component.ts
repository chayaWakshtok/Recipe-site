import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.scss']
})
export class EditUserComponent {

  user!: User;

  constructor(private userService: UserService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (id) {
      this.userService.getUser(id).subscribe(res => {
        this.user = res;
      })
    }
  }

  deleteUser() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.userService.delete(this.user.id).subscribe(res => {
          if (!res) {
            this.toastr.error('Fail to delete user', 'Fail delete!');
          }
          else {
            this.router.navigate(["admin/users"]);
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  updateUser() {
    this.userService.update(this.user).subscribe(res => {
      if (res)
        this.router.navigate(["admin/users"]);
      else
        this.toastr.error('Fail to update user', 'Fail update!');
    })
  }

}
