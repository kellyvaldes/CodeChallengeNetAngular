import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductApiService } from '../../product-api.service';
import { Product } from '../product.dto';

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {

  productList$!:Observable<Product[]>;

  constructor(private service: ProductApiService) { }

  ngOnInit(): void {
    this.productList$ = this.service.getProductList();    
  }

  modalTitle: string = ''
  activateAddEditProductComponent: boolean = false;
  product:any;

  modalAdd() {
    this.product = {
      id:0,
      name:null,
      ageRestriction:null,
      price:null,
      company: null
    }
    this.modalTitle = "Add Product";
    this.activateAddEditProductComponent = true;
  }

  modalEdit(item:any) {
    this.product = item;
    this.modalTitle = "Edit Product";
    this.activateAddEditProductComponent = true;
  }

  delete(item:any) {
    if(confirm(`Are you sure you want to delete product ${item.id}`)) {
      this.service.deleteProduct(item.id).subscribe(res => {
        var closeModalBtn = document.getElementById('add-edit-modal-close');
      if(closeModalBtn) {
        closeModalBtn.click();
      }

      var showDeleteSuccess = document.getElementById('delete-success-alert');
      if(showDeleteSuccess) {
        showDeleteSuccess.style.display = "block";
      }
      setTimeout(function() {
        if(showDeleteSuccess) {
          showDeleteSuccess.style.display = "none"
        }
      }, 4000);
      this.productList$ = this.service.getProductList();
      })
    }
  }

  modalClose() {
    this.activateAddEditProductComponent = false;
    this.productList$ = this.service.getProductList();
  }

}
