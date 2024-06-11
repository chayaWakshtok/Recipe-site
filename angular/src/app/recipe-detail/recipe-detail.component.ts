import { ViewportScroller } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import jsPDF from 'jspdf';
import { Recipe } from '../models/recipe';
import { RecipeService } from '../services/recipe.service';
import { ActivatedRoute } from '@angular/router';
import { StorageService } from '../services/storage.service';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent {

  recipe!: Recipe;
  recipesCategory: Recipe[] = [];
  isLike: boolean = false;

  constructor(private viewportScroller: ViewportScroller,
    public recipeService: RecipeService,
    private activatedRoute: ActivatedRoute,
    private storageService: StorageService
  ) { }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    if (id) {
      this.recipeService.getRecipe(id).subscribe(res => {
        this.recipe = res;
        var id = this.storageService.getUser().id;
        this.recipe.likes
          ? (this.isLike =
            this.recipe.likes.findIndex((x) => x.userId == id) >= 0)
          : (this.isLike = false);
        this.recipeService.getRecipesByCategory(this.recipe.categoryId ?? 0).subscribe(res => {
          this.recipesCategory = res;
        })
      })
    }

  }

  onClick(): void {
    this.viewportScroller.scrollToAnchor("share-item");
  }

  //   comment_id: 87
  // reply_user: 1
  // reply_text: nnnnn
  // reply_posted: 1
  //type: reply

  //   comment_item: 1
  // comment_user: 1
  // comment_text: sdasdas
  // comment_posted: 1
  //type:comment


}
