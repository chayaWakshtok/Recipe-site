import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { forkJoin } from 'rxjs';
import { Recipe } from 'src/app/models/recipe';
import { RecipeService } from 'src/app/services/recipe.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  countUsers = 0;
  countRecipes = 0;
  recipes: Recipe[] = [];

  constructor(private router: Router, private userService: UserService,
    private recipeService: RecipeService) {
  }
  ngOnInit(): void {
    forkJoin(this.userService.getCount(), this.recipeService.getCount(), this.recipeService.mostLike())
      .subscribe((res) => {
        this.countUsers = res[0];
        this.countRecipes = res[1]
        this.recipes = res[2];
      });
  }



}
