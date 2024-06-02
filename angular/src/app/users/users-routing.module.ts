import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProfileComponent } from './profile/profile.component';
import { HomeUsersComponent } from './home-users/home-users.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { MyRecipeComponent } from './my-recipe/my-recipe.component';
import { SubmitRecipeComponent } from './submit-recipe/submit-recipe.component';
import { FollowersComponent } from './followers/followers.component';
import { FollowingComponent } from './following/following.component';

const routes: Routes = [
  { path: "submit-recipe", component: SubmitRecipeComponent },
  {
    path: "", component: HomeUsersComponent, children: [
      { path: "profile", component: ProfileComponent },
      { path: "favorites", component: ProfileComponent },
      { path: "edit-profile", component: EditProfileComponent },
      { path: "my-recipes", component: MyRecipeComponent },
      { path: "followers", component: FollowersComponent },
      { path: "following", component: FollowingComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
