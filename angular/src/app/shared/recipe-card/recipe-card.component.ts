import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { TablerIconsModule } from 'angular-tabler-icons';
import { Recipe } from 'src/app/models/recipe';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
    selector: 'app-recipe-card',
    templateUrl: './recipe-card.component.html',
    styleUrls: ['./recipe-card.component.scss'],
})
export class RecipeCardComponent {

  @Input() showUser: boolean = false;
  @Input() recipe!: Recipe;

  constructor(private route: Router) { }

  goToDetails(id:number)
  {
    this.route.navigate(['/recipe', id]);
  }
}
