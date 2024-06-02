import { NgModule } from '@angular/core';
import { CommonModule, JsonPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { TablerIconsModule } from 'angular-tabler-icons';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgbModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { UsersRoutingModule } from './users-routing.module';
import { ProfileComponent } from './profile/profile.component';
import { EditProfileComponent } from './edit-profile/edit-profile.component';
import { HomeUsersComponent } from './home-users/home-users.component';
import { MyRecipeComponent } from './my-recipe/my-recipe.component';
import { SubmitRecipeComponent } from './submit-recipe/submit-recipe.component';
import { FollowingComponent } from './following/following.component';
import { FollowersComponent } from './followers/followers.component';


@NgModule({
  declarations: [
    ProfileComponent,
    EditProfileComponent,
    HomeUsersComponent,
    MyRecipeComponent,
    SubmitRecipeComponent,
    FollowingComponent,
    FollowersComponent
  ],
  imports: [
    CommonModule,
    UsersRoutingModule,
    FormsModule,
    HttpClientModule,
    TablerIconsModule.pick(TablerIcons),
    DataTablesModule,
    NgbModule,
    NgbTypeaheadModule,
    JsonPipe,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })
  ],
  exports:[
    ProfileComponent
  ]
})
export class UsersModule { }
