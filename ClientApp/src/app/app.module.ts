import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';


import { AppComponent } from './app.component';
import { GlobalModule } from './global/global.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    GlobalModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
