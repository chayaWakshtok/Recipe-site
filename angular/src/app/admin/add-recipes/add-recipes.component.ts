import { HttpParams } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { NgbTypeahead } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { Observable, OperatorFunction, Subject, catchError, debounceTime, distinctUntilChanged, filter, map, merge, of, switchMap, tap } from 'rxjs';
import { Category } from 'src/app/models/category';
import { ETypeCount, Ingredient } from 'src/app/models/ingredient';
import { Instruction } from 'src/app/models/instruction';
import { Product } from 'src/app/models/product';
import { Recipe } from 'src/app/models/recipe';
import { CategoryService } from 'src/app/services/category.service';
import { ProductService } from 'src/app/services/product.service';
import { RecipeService } from 'src/app/services/recipe.service';
import { DifficultiesComponent } from '../difficulties/difficulties.component';
import { DifficultyService } from 'src/app/services/difficulty.service';
import { Difficulty } from 'src/app/models/difficulty';
import { StorageService } from 'src/app/services/storage.service';


@Component({
  selector: 'app-add-recipes',
  templateUrl: './add-recipes.component.html',
  styleUrls: ['./add-recipes.component.scss']
})
export class AddRecipesComponent {
  recipe: Recipe = new Recipe();
  searching = false;
  searchFailed = false;
  unitsList: any[] = [];
  categoriesList: Category[] = [];
  difficultiesList: Difficulty[] = [];
  imageSrc: string = '';

  constructor(
    private storageService: StorageService,
    private recipeService: RecipeService,
    private router: Router,
    private toastr: ToastrService,
    private productService: ProductService,
    private categoryService: CategoryService,
    private difficultyService: DifficultyService
  ) {
    for (var n in ETypeCount) {
      if (typeof ETypeCount[n] === 'number') {
        this.unitsList.push({ id: <any>ETypeCount[n], name: n });
      }
    }

    this.categoryService.getAll().subscribe((res) => {
      this.categoriesList = res;
    });

    this.difficultyService.getAll().subscribe((res) => {
      this.difficultiesList = res;
    });

    this.recipe.ingredients = [];
    this.recipe.ingredients.push(new Ingredient());

    this.recipe.instructions = [];
    this.recipe.instructions.push(new Instruction());
  }

  saveRecipe() {
    this.recipe.ingredients.forEach((p) => {
      if (typeof p.product == 'string') {
        var name = p.product as string;
        p.product = new Product();
        p.product.name = name;
      }
    });
    this.recipe.userId = this.storageService.getUser().id;
    this.recipeService.add(this.recipe).subscribe(
      (res) => {
        if (res) this.router.navigate(['admin/recipes']);
        else this.toastr.error('Fail to save new recipe', 'Fail save!');
      },
      (err) => {
        this.toastr.error('Fail to save new recipe', 'Fail save!');
      }
    );
  }

  formatter = (x: { name: string }) => x.name;

  search: OperatorFunction<string, any> = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => (this.searching = true)),
      switchMap((term) =>
        this.productService.search(term).pipe(
          tap(() => (this.searchFailed = false)),
          catchError(() => {
            this.searchFailed = true;
            return of([]);
          })
        )
      ),
      tap(() => (this.searching = false))
    );

  addIngredients() {
    this.recipe.ingredients.push(new Ingredient());
  }

  removeIngredient(index: number) {
    this.recipe.ingredients.splice(index + 1, 1);
  }

  addInstruction() {
    this.recipe.instructions.push(new Instruction());
  }

  removeInstruction(index: number) {
    this.recipe.instructions.splice(index + 1, 1);
  }

  uploadImage(event: any) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.recipe.imageUrl = reader.result as string;
      };
    }
  }
}


