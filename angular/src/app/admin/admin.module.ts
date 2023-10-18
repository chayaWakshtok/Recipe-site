import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { LoginAdminComponent } from './login-admin/login-admin.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import * as TablerIcons from 'angular-tabler-icons/icons';
import { TablerIconsModule } from 'angular-tabler-icons';
import { SidebarMenuComponent } from './sidebar-menu/sidebar-menu.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UsersComponent } from './users/users.component';
import { AddUserComponent } from './add-user/add-user.component';
import { RecipesComponent } from './recipes/recipes.component';
import { AddRecipesComponent } from './add-recipes/add-recipes.component';
import { CategoriesComponent } from './categories/categories.component';
import { AddCategoryComponent } from './add-category/add-category.component';
import { DifficultiesComponent } from './difficulties/difficulties.component';
import { AddDifficultComponent } from './add-difficult/add-difficult.component';
import { DataTablesModule } from 'angular-datatables';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


@NgModule({
  imports: [
    CommonModule,
    AdminRoutingModule,
    FormsModule,
    HttpClientModule,
    TablerIconsModule.pick(TablerIcons),
    DataTablesModule,
    ToastrModule.forRoot({
      timeOut: 10000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })
  ],
  declarations: [
    HomeAdminComponent,
    LoginAdminComponent,
    SidebarMenuComponent,
    DashboardComponent,
    UsersComponent,
    AddUserComponent,
    RecipesComponent,
    AddRecipesComponent,
    CategoriesComponent,
    AddCategoryComponent,
    DifficultiesComponent,
    AddDifficultComponent
  ],
  exports: []
})
export class AdminModule { }
