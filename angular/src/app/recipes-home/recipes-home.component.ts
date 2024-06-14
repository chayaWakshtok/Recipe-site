import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category';

@Component({
  selector: 'app-recipes-home',
  templateUrl: './recipes-home.component.html',
  styleUrls: ['./recipes-home.component.scss']
})
export class RecipesHomeComponent {

  recipes: Recipe[] = [];
  categories: Category[] = [];
  categorySelected: number = 0;
  searchKey: string = '';

  constructor(private router: Router,
    private recipeService: RecipeService,
    public categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    this.categoryService.getAll().subscribe((res) => {
      this.categories = res;
    })
    this.getRecipes();
  }

  getRecipes() {
    this.recipeService.getAll().subscribe((res) => {
      this.recipes = res;
    })
  }

  search() {

  }
}
