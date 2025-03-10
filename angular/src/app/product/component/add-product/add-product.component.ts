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
  selectedImage: File | null = null;
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
      ImageUrl: new FormControl('',)
    })
  }
  addProduct(){

    const formData = new FormData();
    
    formData.append("name", this.addForm.value.name);
    formData.append("categoryId", this.addForm.value.categoryId);
    formData.append("UnitOfMeasure", this.addForm.value.UnitOfMeasure);

    if (this.selectedImage) {
        formData.append("ImageUrl", this.selectedImage); // הוספת התמונה
    }

    console.log("שליחת מוצר עם קובץ:", formData.getAll);

    
    this._productService.addProduct(formData).subscribe({
      next:(res)=>{
        console.log("המוצר נוסף בהצלחה", res);
        this.productAdded.emit(res);
      },
      error:(err)=>{
        console.log("שגיאה בהוספת מוצר", err.message);
        
      }

    })
  }
  onFileChange(event: any): void {
    if (event.target.files.length > 0) {
        this.selectedImage = event.target.files[0];
    }
}


}
