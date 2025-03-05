import { Component } from '@angular/core';
import { Product, eUnitOfMeasure } from '../../product.model';
import { ProductService } from '../../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-product-id',
  templateUrl: './get-product-id.component.html',
  styleUrl: './get-product-id.component.scss'
})
export class GetProductIdComponent {
id = 0
productDetails!: Product
constructor(private _productService: ProductService, private _router: ActivatedRoute,
   private _r: Router ,private _authService: AuthService) { }
  ngOnInit(): void {
    this._router.params.subscribe((param) => {
      this.id = +param['id'];
      this._productService.getProductById(this.id).subscribe({
        next: (res) => {
          this.productDetails = res;
        },
        error: (err) => {
         console.log("err", err);
         
        }
      })
    });
  }

  public delete(id: number) {
    this._productService.deleteProduct(id).subscribe({
      next: (res) => {
        console.log("delete: ", res);
        this._r.navigate(['product/'])
      },
      error: (err) => {
       console.log("err", err);
       ;
      }
    })
  }

  goBack() {
    this._r.navigate(['product/'])
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isManager(): boolean {
    return this._authService.isManager();
  }


}


