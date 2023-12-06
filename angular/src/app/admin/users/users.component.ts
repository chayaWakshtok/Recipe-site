import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-users',
  encapsulation: ViewEncapsulation.None,
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent {
  title = 'datatables';
  dtOptions: DataTables.Settings = {};
  users: User[] = [];

  constructor(private userService: UserService,
    private cdRef: ChangeDetectorRef,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    this.getUsers();
  }

  getUsers() {
    this.userService.getAll().subscribe(res => {
      this.users = res;
      this.cdRef.detectChanges();
    })
  }

  deleteUser(id: number | undefined) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.userService.delete(id).subscribe(res => {
          if (res) {
            this.users = res;
          }
          else {
            this.toastr.error('Fail to delete user', 'Fail delete!');
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  edit(id: number | undefined) {
    this.router.navigate(["admin/edit_user", id]);
  }
}
