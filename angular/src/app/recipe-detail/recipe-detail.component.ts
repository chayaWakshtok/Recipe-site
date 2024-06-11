import { ViewportScroller } from '@angular/common';
import { Component, ElementRef, ViewChild } from '@angular/core';
import { Recipe } from '../models/recipe';
import { RecipeService } from '../services/recipe.service';
import { ActivatedRoute, Router } from '@angular/router';
import { StorageService } from '../services/storage.service';
import { Like } from '../models/like';

@Component({
  selector: 'app-recipe-detail',
  templateUrl: './recipe-detail.component.html',
  styleUrls: ['./recipe-detail.component.scss']
})
export class RecipeDetailComponent {

  recipe!: Recipe;
  recipesCategory: Recipe[] = [];
  isLike: boolean = false;
  isLogin: boolean = false;

  constructor(private viewportScroller: ViewportScroller,
    public recipeService: RecipeService,
    private activatedRoute: ActivatedRoute,
    private storageService: StorageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const id = Number(this.activatedRoute.snapshot.paramMap.get('id'));
    this.isLogin = this.storageService.isLoggedIn();
    if (id) {
      this.recipeService.getRecipe(id).subscribe(res => {
        this.recipe = res;
        if (this.isLogin) {
          var id = this.storageService.getUser().id;
          this.recipe.likes
            ? (this.isLike =
              this.recipe.likes.findIndex((x) => x.userId == id) >= 0)
            : (this.isLike = false);
        }
        this.recipeService.getRecipesByCategory(this.recipe.categoryId ?? 0).subscribe(res => {
          this.recipesCategory = res;
        })
      })
    }

  }

  onClick(): void {
    this.viewportScroller.scrollToAnchor("share-item");
  }


  removeLike() {
    var id = this.storageService.getUser().id;
    var like = this.recipe.likes?.find((x) => x.userId == id)

  }

  addLike() {
    if (!this.isLogin) {
      this.router.navigate(['/auth/signin']);
    }
    else{
      var like = new Like(this.storageService.getUser().id, this.recipe.id);
      

    }
  }
}
