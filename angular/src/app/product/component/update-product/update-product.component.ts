import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PostProduct, Product, eUnitOfMeasure } from '../../product.model';
import { ProductService } from '../../product.service';
import { Category } from '../../../category/category.model';
import { CategoryService } from '../../../category/category.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.scss'
})
export class UpdateProductComponent implements OnInit {
  @Output() updateProduct = new EventEmitter<Product>();
  @Input() public productUpdate!: Product;
  public product!: PostProduct;
  categories!: Category[];
  eUnitOfMeasure = eUnitOfMeasure;
  unitOptions = Object.entries(eUnitOfMeasure)
    .filter(([key, value]) => isNaN(Number(key))) // סינון המפתחות שהם מספרים
    .map(([key, value]) => ({ key, value })); // המרה למערך של אובייקטים

  constructor(
    private _productService: ProductService,
    private _categoryService: CategoryService
  ) { }

  ngOnInit(): void {
    // אתחול המוצר עם נתוני productUpdate
    if (this.productUpdate) {
      this.product = {
        name: this.productUpdate.name,
        categoryId: this.productUpdate.category.id,
        UnitOfMeasure: this.productUpdate.UnitOfMeasure
      };
    }

    // הבאת נתוני המוצר מהשרת
    this._productService.getProductById(this.productUpdate.id).subscribe({
      next: (res) => {
        console.log("Product received:", res);
        this.product.name = res.name;
        this.product.categoryId = res.category.id;
        this.product.UnitOfMeasure = res.UnitOfMeasure;
      },
      error: (err) => {
        console.log("Error fetching product:", err);
      }
    });

    // הבאת רשימת קטגוריות
    this._categoryService.getCategoriesFromServer().subscribe({
      next: (res) => {
        this.categories = res;
      },
      error: (err) => {
        console.log("לא הצלחתי להביא את הקטגוריות", err);
      }
    });
  }

  saveChanges() {
    console.log("Product before update:", this.product);

    this._productService.updateProduct(this.productUpdate.id, this.product).subscribe({
      next: () => {
        console.log("העדכון עבר בהצלחה");
        this.updateProduct.emit(this.productUpdate); // שליחת המוצר המעודכן
      },
      error: (err) => {
        console.log("עדכון מוצר נכשל", err);
      }
    });
  }
}
