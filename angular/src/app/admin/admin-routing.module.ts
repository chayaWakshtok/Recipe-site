import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { LoginAdminComponent } from './login-admin/login-admin.component';
import { AuthAdminGuard } from '../helpers/auth-admin.guard';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RecipesComponent } from './recipes/recipes.component';
import { AddRecipesComponent } from './add-recipes/add-recipes.component';
import { CategoriesComponent } from './categories/categories.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { AddDifficultComponent } from './add-difficult/add-difficult.component';
import { DifficultiesComponent } from './difficulties/difficulties.component';
import { UsersComponent } from './users/users.component';
import { AddUserComponent } from './add-user/add-user.component';
import { EditUserComponent } from './edit-user/edit-user.component';
import { EditCategoryComponent } from './edit-category/edit-category.component';
import { EditDifficultComponent } from './edit-difficult/edit-difficult.component';
import { CommentsComponent } from './comments/comments.component';

const routes: Routes = [
  {
    path: "", component: HomeAdminComponent, canActivate: [AuthAdminGuard],
    children: [
      { path: "", component: DashboardComponent },
      { path: "recipes", component: RecipesComponent },
      { path: "new_recipe", component: AddRecipesComponent },
      { path: "edit_recipe/:id", component: AddRecipesComponent },
      { path: "categories", component: CategoriesComponent },
      { path: "new_category", component: AddCategoryComponent },
      { path: "edit_category/:id", component: EditCategoryComponent },
      { path: "difficulties", component: DifficultiesComponent },
      { path: "new_difficult", component: AddDifficultComponent },
      { path: "edit_difficult/:id", component: EditDifficultComponent },
      { path: "users", component: UsersComponent },
      { path: "new_user", component: AddUserComponent },
      { path: "edit_user/:id", component: EditUserComponent },
      {path:"comments", component: CommentsComponent },
    ]
  },
  { path: "login", component: LoginAdminComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
