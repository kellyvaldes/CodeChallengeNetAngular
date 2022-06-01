import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ShowProductComponent } from './product/show-product/show-product.component';
import { AddEditProductComponent } from './product/add-edit-product/add-edit-product.component';

import { ProductApiService } from './product-api.service';

@NgModule({
  declarations: [
    AppComponent,
    ShowProductComponent,
    AddEditProductComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],  
  providers: [
    ProductApiService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
