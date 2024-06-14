import { Component, Input } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';

@Component({
  selector: 'app-my-recipe-card',
  templateUrl: './my-recipe-card.component.html',
  styleUrls: ['./my-recipe-card.component.scss']
})
export class MyRecipeCardComponent {

  @Input() recipe!: Recipe;
}
