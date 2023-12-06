import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Component, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { Category } from 'src/app/models/category';
import { User } from 'src/app/models/user';
import { CategoryService } from 'src/app/services/category.service';
import { UserService } from 'src/app/services/user.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss']
})
export class CategoriesComponent {

  title = 'datatables';
  dtOptions: DataTables.Settings = {};
  categories: Category[] = [];

  constructor(private categoryService: CategoryService,
    private cdRef: ChangeDetectorRef,
    private toastr: ToastrService,
    private router: Router) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers'
    };

    this.getCategories();
  }

  getCategories() {
    this.categoryService.getAll().subscribe(res => {
      this.categories = res;
      this.cdRef.detectChanges();
    })
  }

  delete(id: number | undefined) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.categoryService.delete(id).subscribe(res => {
          if (res) {
            this.categories = res;
          }
          else {
            this.toastr.error('Fail to delete category', 'Fail delete!');
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  edit(id: number | undefined) {
    this.router.navigate(["admin/edit_category", id]);
  }

}
