import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html',
  styleUrls: ['./recipes.component.scss']
})
export class RecipesComponent {

  dtOptions: DataTables.Settings = {};
  recipes: Recipe[] = [];

  constructor(private recipeService: RecipeService,
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
    this.recipeService.getAll().subscribe(res => {
      this.recipes = res;
      this.cdRef.detectChanges();
    })
  }

  deleteRecipe(id: number | undefined) {
    Swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this item!',
      icon: 'error',
      showCancelButton: true,
      confirmButtonText: 'YES, DELETE IT!',
      cancelButtonText: 'CANCEL',
    }).then((result) => {
      if (result.value) {
        this.recipeService.delete(id).subscribe(res => {
          if (res) {
            this.recipes = res;
          }
          else {
            this.toastr.error('Fail to delete recipe', 'Fail delete!');
          }
        },err=>{
          this.toastr.error('Fail to delete user Before Need Remove User Data', 'Fail delete!');
        })
      } else if (result.dismiss === Swal.DismissReason.cancel) {
      }
    });
  }
}
