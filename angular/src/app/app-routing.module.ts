import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './auth/register/register.component';
import { HomePageComponent } from './home-page/home-page.component';
import { RecipesHomeComponent } from './recipes-home/recipes-home.component';
import { SearchComponent } from './search/search.component';
import { MembersComponent } from './members/members.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { SiteContainerComponent } from './site-container/site-container.component';
import { RecipeDetailComponent } from './recipe-detail/recipe-detail.component';

const routes: Routes = [
  { path: 'auth', loadChildren: () => import('./auth/auth.module').then(m => m.AuthModule) },
  {
    path: '', component: SiteContainerComponent, children: [
      { path: 'users', loadChildren: () => import('./users/users.module').then(m => m.UsersModule) },
      { path: 'recipe/:id', component: RecipeDetailComponent },
      { path: 'recipes', component: RecipesHomeComponent },
      { path: 'recipes/:user/:query', component: RecipesHomeComponent },
      { path: 'search', component: SearchComponent },
      { path: 'members', component: MembersComponent },
      { path: 'user/:id', component: UserDetailComponent },
    ]
  },
  { path: '', component: HomePageComponent },
  { path: 'home', component: HomePageComponent },
  { path: 'admin', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
