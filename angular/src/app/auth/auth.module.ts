import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthRoutingModule } from './auth-routing.module';



@NgModule({
    imports: [
        CommonModule,
        AuthRoutingModule,

    ],
    declarations:[
      LoginComponent,
      RegisterComponent
    ],
    exports: [
        LoginComponent,
        RegisterComponent
    ]
})
export class AuthModule { }
