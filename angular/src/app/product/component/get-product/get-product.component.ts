import { Component, OnInit } from '@angular/core';
import { Product } from '../../product.model';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from '../../product.service';
import { AuthService } from '../../../auth/auth.service';
import { CartService } from '../../../cart/cart.service';
import { Category } from '../../../category/category.model';
import { CategoryService } from '../../../category/category.service';


@Component({
  selector: 'app-get-product',
  templateUrl: './get-product.component.html',
  styleUrls: ['./get-product.component.scss'],
})
export class GetProductComponent implements OnInit {
  products: Product[] = [];  // בהתחלה, משאיר את המערך ריק
  allProducts: Product[] = []
  searchList: Product []=[]
  showAdd = false;
  showUpdate = false;
  selectedProduct: Product | null = null;  // אם נבחר מוצר, ניתן לעדכן
  message = '';
  productToUpdate: Product | null = null;  // גם אם מדובר במוצר שאנחנו מעדכנים
  isShow = false;
  categories!: Category[]
  activeCategory: string | null = null;
  searchTerm: string = '';
  popupMessage: string = '';
  isPopupVisible = false;



  constructor(
    private _router: Router,
    private route: ActivatedRoute,
    private _productService: ProductService,
    private _authService: AuthService,
    private _cartService: CartService,
    private _categoryService: CategoryService,
   ) { }


  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.activeCategory = params.get('category');
      const searchWord = params.get('word');
      this.getProducts(() => { // מחכה שהמוצרים ייטענו קודם
        if (this.activeCategory) {
          this.filterProductsByCategory();
        }else if (searchWord) {
          this.search(searchWord); // מבצע חיפוש דרך ה-URL
        }
      });
    });

    this._categoryService.getCategoriesFromServer().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: (err) => {
        console.log("לא הצלחתי להביא את הקטגוריות", err);
      }
    });
  }

  

  getProducts(callback?: () => void): void {
    this._productService.getProducts().subscribe({
      next: (res) => {
        this.products = res;
        this.allProducts = res;
        console.log("המוצרים נטענו בהצלחה", this.products);

        if (callback) { // אם יש פונקציה נוספת שצריך לקרוא לה
          callback();
        }
      },
      error: (err) => {
        this.message = err;
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
    this.isShow = true;  // מגדירים את ה- isShow להיות
    this._router.navigate(['product/get-product-id', id]);  // מעבירים לעמוד של פרטי המוצר
  }
  addProductToCart(product: Product) {
    const shoppingCart = {
      name: product.name,
      categoryId: product.category.id,
      UnitOfMeasure: product.UnitOfMeasure
    };
    this._cartService.addProduct(this.getUserId(), shoppingCart).subscribe({
      next: (res) => {
        console.log("המוצר נוסף בהצלחה", res);
        this.popupMessage = `המוצר "${product.name}" התווסף לסל הקניות! 🎉`;
        this.isPopupVisible = true;
        setTimeout(() => this.isPopupVisible = false, 2000);
      },
      error: (err) => {
        console.log("המוצר לא הצליח להתווסף");

      }
    })
  }
  private getUserId(): number {
    const authDataString = localStorage.getItem('authToken');
    if (authDataString) {
      try {
        const authData = JSON.parse(authDataString);
        return authData.userId || 0;
      } catch (error) {
        console.error("שגיאה בפרסור authToken:", error);
      }
    }
    return 0;
  }

  getProductByCategory(category: string) {
    this._router.navigate(['product/get-product', category])
  }
  filterProductsByCategory() {
    this.products = this.allProducts.filter(p => p.category.name === this.activeCategory);
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isUser(): boolean {
    return this._authService.isUser();
  }
  goToSearch(word:string){
    this._router.navigate(['product/get-product/search', word]);

  }
  search(word:string){
    this._productService.search(word).subscribe({
      next:(res)=>{
        console.log("הסינון הצליח", res);
        this.products = res;
      },
      error:(err)=>{
        if (err.status === 404) {
          console.log("לא נמצאו מוצרים עבור החיפוש:", word);
          this.products = []; // אופציונלי: ניקוי המוצרים במקרה של 404
        } else {
          console.log("שגיאה אחרת התרחשה:", err);
        }
      }
    })
  }
}
