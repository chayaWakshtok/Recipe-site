import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Category } from 'src/app/models/category';
import { CategoryService } from 'src/app/services/category.service';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.scss']
})
export class AddCategoryComponent {

  category: Category = new Category(0, "", "", "", 1);
  imageSrc: string = '';

  constructor(private catService: CategoryService,
    private router: Router,
    private toastr: ToastrService) {

  }

  saveCat() {
    this.catService.add(this.category).subscribe(res => {
      if (res.length > 0)
        this.router.navigate(["admin/categories"]);
      else {
        this.toastr.error('Fail to save new category', 'Fail save!');

      }
    }, err => {
      this.toastr.error('Fail to save new category', 'Fail save!');
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
