import { Component } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';

@Component({
  selector: 'app-my-recipe',
  templateUrl: './my-recipe.component.html',
  styleUrls: ['./my-recipe.component.scss']
})
export class MyRecipeComponent {

  dtOptions: DataTables.Settings = {};
  recipes: Recipe[] = [];

  constructor(public recipeService: RecipeService) { }

  ngOnInit(): void {
    this.recipeService.getMyRecipes().subscribe(res => {
      this.recipes = res;
    })
  }

  delete(id: number) {
    this.recipeService.delete(id).subscribe(res => {
      if (!res) {
      }
      else {
        this.recipeService.getMyRecipes().subscribe(res => {
          this.recipes = res;
        })
      }
    })

  }
}
