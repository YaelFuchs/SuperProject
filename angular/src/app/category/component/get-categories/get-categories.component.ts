import { Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../../category.model';
import { CategoryService } from '../../category.service';


@Component({
  selector: 'app-get-categories',
  templateUrl: './get-categories.component.html',
  styleUrl: './get-categories.component.scss'
})
export class GetCategoriesComponent implements OnInit{
   categories!: Category[]
   showAdd = false
   showUpdate = false
   message=''
   categoryToUpdate! : Category
   isShow: boolean = false

   ngOnInit() {
    this.getCategories();
     
   }
  constructor(private _router: Router, private _categoryService: CategoryService){}

   getCategories(){
   this._categoryService.getCategoriesFromServer().subscribe({
    next: (res)=>{
      console.log('res'+ res);

      this.categories = res;
      console.log(this.categories);
      
    },
    error:(err)=>{
      this.message = err;
    }
   })
   }
   onCategoryAdded(category: Category){
    this.showAdd = false;
    this.getCategories();
   }
 update(category: Category){
  this.categoryToUpdate = category;
  this.showUpdate = true;

 }
 onUpdateCategory(category: Category) {
    this.showUpdate = false;
    this.getCategories();  // קריאה לפונקציה כדי לעדכן את רשימת הקטגוריות
 
}
showDetailes(id: number){
  this._router.navigate(['get-category-id',id])
}
}

