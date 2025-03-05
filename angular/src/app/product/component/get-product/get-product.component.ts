import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { Product } from '../../product.model';
import { Router } from '@angular/router';
import { ProductService } from '../../product.service';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-product',
  templateUrl: './get-product.component.html',
  styleUrl: './get-product.component.scss'
})
export class GetProductComponent implements OnInit {
  products!: Product[]
  showAdd = false;
  showUpdate = false;
  selectedProduct!: Product
  message = ''
  productToUpdate!: Product
  isShow: boolean = false

  ngOnInit(): void {
    this.getProducts();
  }

  constructor(private _router: Router,
    private _productService: ProductService,
    private __authService: AuthService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  getProducts() {
    this._productService.getProducts().subscribe({
      next: (res) => {
        console.log("קבלת המוצרים עברה בהצלחה", res);
        this.products = res;
        console.log(this.products);

      },
      error: (err) => {
        this.message = err;
      }
    })
  }
  onProductAdded(product : Product){
    this.showAdd = false;
    this.getProducts();
  }
  update(product : Product){
    this.productToUpdate = product;
    this.showUpdate = true;
  }
  onUpdateProduct(){
    this.showUpdate = false;
    this.getProducts();
  }
  showDetailes(id: number){
    this.isShow=true;
    this._router.navigate(['product/get-product-id',id]);
  }
  isManager(): boolean{
    return this.__authService.isManager();
  }
  isAdmin(): boolean {
    return this.__authService.isAdmin();
  }

}
