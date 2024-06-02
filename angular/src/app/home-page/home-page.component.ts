import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../shared/footer/footer.component';
import { CategoryCardComponent } from '../shared/category-card/category-card.component';
import { RecipeCardComponent } from '../shared/recipe-card/recipe-card.component';
import { RecipeScrollCardComponent } from '../shared/recipe-scroll-card/recipe-scroll-card.component';
import { Category } from '../models/category';
import { CategoryService } from '../services/category.service';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent implements OnInit {

  categories: Category[] = []
  latestRecipes: Recipe[]=[];
  likesRecipes: Recipe[]=[];

  constructor(public categoryService: CategoryService,
    public recipeService:RecipeService) {
    categoryService.getAll().subscribe(
      data => {
        this.categories = data;
      }
    )
  }

  ngOnInit(): void {
    this.recipeService.getLaters().subscribe(
      data => {
        this.latestRecipes = data;
      }
    );

    this.recipeService.mostLike().subscribe(
      data => {
        this.likesRecipes = data;
      }
    );
  }

}
