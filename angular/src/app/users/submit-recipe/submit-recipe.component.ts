import { Component } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { StorageService } from 'src/app/services/storage.service';

@Component({
  selector: 'app-submit-recipe',
  templateUrl: './submit-recipe.component.html',
  styleUrls: ['./submit-recipe.component.scss']
})
export class SubmitRecipeComponent {

  recipe: Recipe = new Recipe();
  message: string = '';
  error: boolean = false;
  ingredients: string = "";
  instructions: string = "";
  imageSrc: string = "";

  constructor(public recipeService: RecipeService,
    public storageService: StorageService
  ) { }

  ngOnInit(): void {
    this.message = '';
    this.error = false;
  }

  save() {
    // this.recipe.ingredients = this.ingredients;
    // this.recipe.instructions = this.instructions;

    this.recipe.userId = this.storageService.getUser().id;

    this.recipeService.add(this.recipe).subscribe(data => {
      this.message = "Your recipe was successfully added";
      this.recipe = new Recipe();
    }, err => {
      console.log(err);
      this.error = true;
    });
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
