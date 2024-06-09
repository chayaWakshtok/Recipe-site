import { Component, Input } from '@angular/core';
import { Recipe } from 'src/app/models/recipe';

@Component({
  selector: 'app-recipe-detail-like',
  templateUrl: './recipe-detail-like.component.html',
  styleUrls: ['./recipe-detail-like.component.scss']
})
export class RecipeDetailLikeComponent {

  @Input() recipe!: Recipe;
}
