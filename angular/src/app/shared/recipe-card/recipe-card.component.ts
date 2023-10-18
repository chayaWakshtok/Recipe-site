import { Component, Input } from '@angular/core';
import { NgIf } from '@angular/common';
import { TablerIconsModule } from 'angular-tabler-icons';

@Component({
    selector: 'app-recipe-card',
    templateUrl: './recipe-card.component.html',
    styleUrls: ['./recipe-card.component.scss'],
})
export class RecipeCardComponent {

  @Input() showUser: boolean = false;
}
