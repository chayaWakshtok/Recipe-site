import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-edit-recipe',
  templateUrl: './edit-recipe.component.html',
  styleUrls: ['./edit-recipe.component.scss']
})
export class EditRecipeComponent implements OnInit {

  recipe!: Recipe;
  imageSrc: string = "";
  constructor(public recipeservice: RecipeService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (id) {
      this.recipeservice.getRecipe(id).subscribe(res => {
        this.recipe = res;
        this.imageSrc = res.imageUrl ?? "";
      })
    }
  }

  delete() {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.recipeservice.delete(this.recipe.id).subscribe(res => {
          if (!res) {
            this.toastr.error('Fail to delete recipe', 'Fail delete!');
          }
          else {
            this.router.navigate(["admin/recipes"]);
          }
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }

  update() {
    this.recipeservice.update(this.recipe).subscribe(res => {
      if (res)
        this.router.navigate(["admin/recipes"]);
      else
        this.toastr.error('Fail to update recipe', 'Fail update!');
    })
  }

  uploadImage(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.recipe.imageUrl = reader.result as string;
      }
    }
  }

}
