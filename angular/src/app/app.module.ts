import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { TablerIconsModule } from 'angular-tabler-icons';
import { AuthModule } from './auth/auth.module';
import { HomePageComponent } from './home-page/home-page.component';
import { SharedModule } from './shared/shared.module';
import { AdminModule } from './admin/admin.module';
import { httpInterceptorProviders } from './helpers/http.interceptor';
import { HttpClientModule } from '@angular/common/http';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { OverlayModule } from '@angular/cdk/overlay';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule, NgbTypeaheadModule } from '@ng-bootstrap/ng-bootstrap';
import { JsonPipe } from '@angular/common';
import { UsersModule } from './users/users.module';
import { SearchComponent } from './search/search.component';
import { RecipesHomeComponent } from './recipes-home/recipes-home.component';
import { MembersComponent } from './members/members.component';
import { UserDetailComponent } from './user-detail/user-detail.component';
import { SiteContainerComponent } from './site-container/site-container.component';
import { RouterModule } from '@angular/router';
import { RecipeDetailComponent } from './recipe-detail/recipe-detail.component';


@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    SearchComponent,
    RecipesHomeComponent,
    MembersComponent,
    UserDetailComponent,
    SiteContainerComponent,
    RecipeDetailComponent
  ],
  imports: [
    TablerIconsModule.pick(TablerIcons),
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AuthModule,
    OverlayModule,
    SharedModule,
    AdminModule,
    UsersModule,
    ReactiveFormsModule,
    DataTablesModule,
    NgbModule,
    RouterModule,
    NgbTypeaheadModule, FormsModule, JsonPipe,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })],
  providers: [httpInterceptorProviders],
  bootstrap: [AppComponent]
})
export class AppModule { }
