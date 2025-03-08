import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PostProduct, Product, eUnitOfMeasure } from '../../product.model';
import {ProductService } from '../../product.service';
import { Category } from '../../../category/category.model';
import { CategoryService } from '../../../category/category.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrl: './update-product.component.scss'
})
export class UpdateProductComponent implements OnInit{
  @Output() updateProduct = new EventEmitter<Product>;
  @Input() public productUpdate!: Product
  public product!:PostProduct
  p!:Product
  categories!:Category[]
  eUnitOfMeasure = eUnitOfMeasure;
  unitOptions = Object.entries(eUnitOfMeasure)
  .filter(([key, value]) => isNaN(Number(key))) // סינון המפתחות שהם מספרים
  .map(([key, value]) => ({ key, value })); // המרה למערך של אובייקטים


  constructor(private _productService: ProductService,  private _categoryService : CategoryService,){}
  ngOnInit(): void {
    // this.product = {
    //   id: this.productUpdate.id,
    //    name : this.productUpdate.name,
    //    category: this.productUpdate.category,
    //    UnitOfMeasure: this.productUpdate.UnitOfMeasure
    // }
    this._productService.getProductById(this.productUpdate.id).subscribe({
      next: (res) => {
        this.p = res;
        console.log("ressssssss", res);
        
      },
      error: (err) => {
        console.log("err", err);
      }
    });
    console.log("p================:", this.p);
    console.log("UnitOfMeasure in productUpdate:", this.p.UnitOfMeasure);
 
 
    this._categoryService.getCategoriesFromServer().subscribe({
      next:(res)=>{
        this.categories = res;
      },
      error:(err)=>{
        console.log("לא הצלחתי להביא את הקטגוריות", err);
        
      }
    })
  }

  saveChanges(){
    this.product = {
      name : this.product.name,
       categoryId:Number(this.p.category.id) ,
       UnitOfMeasure: Number(this.product.UnitOfMeasure)
    }
    console.log("המוצר לעדכון:", this.product);
    this._productService.updateProduct(this.productUpdate.id, this.product).subscribe({
      next:(res)=>{
        console.log("העדכון עבר בהצלחה");
        this.updateProduct.emit();
    },
    error:(err)=>{
      console.log("עדכון מוצר נכשל", err);
      
    }
    })
  }

}
