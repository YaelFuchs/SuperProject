import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { Product } from '../../product.model';
import { Router } from '@angular/router';
import { ProductService } from '../../product.service';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-product',
  templateUrl: './get-product.component.html',
  styleUrls: ['./get-product.component.scss']
})
export class GetProductComponent implements OnInit {
  products: Product[] = [];  // בהתחלה, משאיר את המערך ריק
  showAdd = false;
  showUpdate = false;
  selectedProduct: Product | null = null;  // אם נבחר מוצר, ניתן לעדכן
  message = '';
  productToUpdate: Product | null = null;  // גם אם מדובר במוצר שאנחנו מעדכנים
  isShow = false;

  constructor(
    private _router: Router,
    private _productService: ProductService,
    private __authService: AuthService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts(): void {
    this._productService.getProducts().subscribe({
      next: (res) => {
        console.log("קבלת המוצרים עברה בהצלחה", res);
        this.products = res; // מקבל את המוצרים
        console.log(this.products);
      },
      error: (err) => {
        this.message = err;  // אם יש בעיה, נשמור את השגיאה במסר
      }
    });
  }

  onProductAdded(product: Product): void {
    this.showAdd = false;
    this.getProducts();  // אחרי הוספת מוצר, נטען את המוצרים מחדש
  }

  update(product: Product): void {
    this.productToUpdate = product;
    this.showUpdate = true;  // נציג את הטופס לעדכון
  }

  onUpdateProduct(): void {
    this.showUpdate = false;
    this.getProducts();  // אחרי עדכון, נטען את המוצרים מחדש
  }

  showDetailes(id: number): void {
    this.isShow = true;  // מגדירים את ה- isShow להיות true כדי להציג פרטי מוצר
    this._router.navigate(['product/get-product-id', id]);  // מעבירים לעמוד של פרטי המוצר
  }

  isManager(): boolean {
    return this.__authService.isManager();  // מחזיר true אם המשתמש הוא מנהל
  }

  isAdmin(): boolean {
    return this.__authService.isAdmin();  // מחזיר true אם המשתמש הוא אדמין
  }
}
