import { Component, Input } from '@angular/core';
import { TablerIconsModule } from 'angular-tabler-icons';
import { Recipe } from 'src/app/models/recipe';

@Component({
    selector: 'app-recipe-scroll-card',
    templateUrl: './recipe-scroll-card.component.html',
    styleUrls: ['./recipe-scroll-card.component.scss'],
})
export class RecipeScrollCardComponent {

    @Input() recipe!:Recipe;
}
