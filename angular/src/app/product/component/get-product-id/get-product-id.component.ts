import { Component } from '@angular/core';
import { Product } from '../../product.model';
import { ProductService } from '../../product.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';  // הוספת AuthService

@Component({
  selector: 'app-get-product-id',
  templateUrl: './get-product-id.component.html',
  styleUrls: ['./get-product-id.component.scss']  // שים לב ל-styleUrls ולא styleUrl
})
export class GetProductIdComponent {
  id = 0;
  productDetails!: Product;

  constructor(
    private _productService: ProductService,
    private _router: ActivatedRoute,
    private _r: Router,
    private _authService: AuthService  // הוספת AuthService לקונסטרקטור
  ) { }

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
      });
    });
  }

  public delete(id: number) {
    this._productService.deleteProduct(id).subscribe({
      next: (res) => {
        console.log("delete: ", res);
        this._r.navigate(['product/']);
      },
      error: (err) => {
        console.log("err", err);
      }
    });
  }
  goBack() {
    this._r.navigate(['product/']);
  }
  isManager(): boolean {
    return this._authService.isManager();
  }
}
