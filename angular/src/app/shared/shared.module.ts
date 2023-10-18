import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';
import { RecipeCardComponent } from './recipe-card/recipe-card.component';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { TablerIconsModule } from 'angular-tabler-icons';
import { RecipeScrollCardComponent } from './recipe-scroll-card/recipe-scroll-card.component';
import { CategoryCardComponent } from './category-card/category-card.component';

@NgModule({
  imports: [
    CommonModule,
    TablerIconsModule.pick(TablerIcons),

  ],
  declarations: [
    FooterComponent,
    HeaderComponent,
    RecipeCardComponent,
    RecipeScrollCardComponent,
    CategoryCardComponent,
  ],
  exports: [
    FooterComponent,
    HeaderComponent,
    RecipeCardComponent,
    RecipeScrollCardComponent,
    CategoryCardComponent
  ]
})
export class SharedModule { }
