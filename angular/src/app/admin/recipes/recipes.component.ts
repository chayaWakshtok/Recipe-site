import { ChangeDetectorRef, Component } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

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
}
