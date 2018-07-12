import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';

import { GlobalRoutingModule } from './global-routing.module';
import { LoginComponent } from './login/login.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { RegistrationComponent } from './registration/registration.component';

@NgModule({
  imports: [    
    RouterModule,
    FormsModule,
    CommonModule,
    HttpClientModule,
    GlobalRoutingModule,
    ReactiveFormsModule
  ],
  declarations: [
    LoginComponent, 
    NotFoundComponent,
    RegistrationComponent],
  exports:[
    RouterModule,
    FormsModule,
  ]
})
export class GlobalModule { }
