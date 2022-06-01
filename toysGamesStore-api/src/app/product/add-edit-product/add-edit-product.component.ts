import { Component, Input, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductApiService } from 'src/app/product-api.service';
import { Product } from '../product.dto';

import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css']
})
export class AddEditProductComponent implements OnInit {

  productList$!: Observable<Product[]>;

  addEditProductForm = this.fb.group({    
    id:[''],
    name: [''],
    price: [''],
    company: [''],
    ageRestriction: [''],
    description: [''],
    lastName: ['']    
  });

  @Input() product:any;

  ngOnInit(): void {
    this.productList$ = this.service.getProductList();
    
    this.addEditProductForm = this.fb.group({
      id: this.product.id,
      name: this.product.name,
      price: this.product.price,
      company: this.product.company,
      ageRestriction: this.product.ageRestriction,
      description: this.product.description
    });
  }

  addProduct() {
    var product = {
      description: this.addEditProductForm.get('description')?.value,
      name: this.addEditProductForm.get('name')?.value,
      ageRestriction: this.addEditProductForm.get('ageRestriction')?.value,
      company: this.addEditProductForm.get('company')?.value,
      price: this.addEditProductForm.get('price')?.value
    };
    this.service.addProduct(product).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showAddSuccess = document.getElementById('add-success-alert');
      if(showAddSuccess) {
        showAddSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showAddSuccess) {
          showAddSuccess.style.display = "none"
        }
      }, 4000);
    })
  }

  updateProduct() {
    var product = {
      description: this.addEditProductForm.get('description')?.value,
      name: this.addEditProductForm.get('name')?.value,
      ageRestriction: this.addEditProductForm.get('ageRestriction')?.value,
      company: this.addEditProductForm.get('company')?.value,
      price: this.addEditProductForm.get('price')?.value
      /*description:this.description,*/
      /*name: this.name,
      ageRestriction: this.ageRestriction,
      company: this.company,
      price: this.price*/
    };
    this.service.updateProduct(this.addEditProductForm.get('id')?.value,product).subscribe(res => {
      var closeModalBtn = document.getElementById('add-edit-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showUpdateSuccess = document.getElementById('update-success-alert');
      if(showUpdateSuccess) {
        showUpdateSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showUpdateSuccess) {
          showUpdateSuccess.style.display = "none"
        }
      }, 4000);
    })

  }

  onSubmit() {
    // TODO: Use EventEmitter with form value
    console.warn(this.addEditProductForm.value);
  }

  constructor(private service:ProductApiService,
    private fb: FormBuilder) { }
}
