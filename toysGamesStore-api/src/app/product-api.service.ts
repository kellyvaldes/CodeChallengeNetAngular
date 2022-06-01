import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from './product/product.dto';

@Injectable({
  providedIn: 'root'
})
export class ProductApiService {
  readonly productAPIUrl = 'https://localhost:7035/api';

  constructor(private http:HttpClient) { }

  getProductList(): Observable<Product[]>{
    return this.http.get<Product[]>(this.productAPIUrl + '/products');
  }

  addProduct(data: Product){
    return this.http.post(this.productAPIUrl + '/products', data);
  }

  updateProduct(id:number|string, data: Product){
    return this.http.put(this.productAPIUrl + `/products/${id}`, data);
  }

  deleteProduct(id:number|string){
    return this.http.delete(this.productAPIUrl + `/products/${id}`);
  }

}
