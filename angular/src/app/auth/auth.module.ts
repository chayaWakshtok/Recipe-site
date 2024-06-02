import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthRoutingModule } from './auth-routing.module';
import { RouterModule } from '@angular/router';
import { ForgotComponent } from './forgot/forgot.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  imports: [
    CommonModule,
    AuthRoutingModule,
    RouterModule,
    FormsModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    ForgotComponent
  ],
  exports: [
    LoginComponent,
    RegisterComponent,
    ForgotComponent
  ]
})
export class AuthModule { }
