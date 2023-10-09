import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminRoutingModule } from './admin-routing.module';
import { HomeAdminComponent } from './home-admin/home-admin.component';
import { LoginAdminComponent } from './login-admin/login-admin.component';



@NgModule({
  declarations: [
    HomeAdminComponent,
    LoginAdminComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ],
  exports:[

  ]
})
export class AdminModule { }
