import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Product, eUnitOfMeasure } from '../../product.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProductService } from '../../product.service';
import { CategoryService } from '../../../category/category.service';
import { Category } from '../../../category/category.model';

@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrl: './add-product.component.scss'
})
export class AddProductComponent implements OnInit{
  @Output() productAdded = new EventEmitter<Product>();
  public addForm! : FormGroup;
  eUnitOfMeasure = eUnitOfMeasure;
  categories!: Category[];
  // אתחול ישיר במקום בקונסטרקטור
  unitOptions = Object.entries(eUnitOfMeasure)
    .filter(([key, value]) => isNaN(Number(key))) // סינון המפתחות שהם מספרים
    .map(([key, value]) => ({ key, value })); // המרה למערך של אובייקטים

  constructor(private _productService: ProductService, private _categoryService: CategoryService){}

  ngOnInit(): void {
    console.log("ההמרה של האינם:", this.unitOptions);
    
    this._categoryService.getCategoriesFromServer().subscribe({
      next:(res)=>{
        this.categories = res;
      },
      error:(err)=>{
        console.log("לא הצלחתי להביא את הקטגוריות", err);
        
      }
    })
    this.addForm = new FormGroup({
      name: new FormControl('', Validators.required),
      categoryId: new FormControl('', Validators.required),
      UnitOfMeasure: new FormControl('', Validators.required),
    })
  }
  addProduct(){
    const productToSend = {
        name: this.addForm.value.name,
        categoryId: Number(this.addForm.value.categoryId),
        UnitOfMeasure: Number(this.addForm.value.UnitOfMeasure)
    };

    console.log("המוצר החדש:", productToSend);
    
    this._productService.addProduct(productToSend).subscribe({
      next:(res)=>{
        console.log("המוצר נוסף בהצלחה", res);
        this.productAdded.emit(res);
      },
      error:(err)=>{
        console.log("שגיאה בהוספת מוצר", err.message);
        
      }

    })
  }

}
