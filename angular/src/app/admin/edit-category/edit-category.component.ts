import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';
import { CategoryCardComponent } from 'src/app/shared/category-card/category-card.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.scss']
})
export class EditCategoryComponent {

  category!: Category;
  imageSrc: string = "";

  constructor(private catService: CategoryService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (id) {
      this.catService.getCat(id).subscribe(res => {
        this.category = res;
        this.imageSrc = res.image ?? "";
      })
    }
  }

  deleteCat() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.catService.delete(this.category.id).subscribe(res => {
          if (!res) {
            this.toastr.error('Fail to delete category', 'Fail delete!');
          }
          else {
            this.router.navigate(["admin/categories"]);
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  updateCat() {
    this.catService.update(this.category).subscribe(res => {
      if (res)
        this.router.navigate(["admin/categories"]);
      else
        this.toastr.error('Fail to update category', 'Fail update!');
    })
  }

  uploadImage(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.category.image = reader.result as string;
      }
    }
  }

}
