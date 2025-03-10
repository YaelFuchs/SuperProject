import { Component,OnInit} from '@angular/core';
import { Router } from '@angular/router';
import { Category } from '../../category.model';
import { CategoryService } from '../../category.service';
import { AuthService } from '../../../auth/auth.service';


@Component({
  selector: 'app-get-categories',
  templateUrl: './get-categories.component.html',
  styleUrl: './get-categories.component.scss'
})
export class GetCategoriesComponent implements OnInit{
   categories!: Category[]
   showAdd = false
   showUpdate = false
   selectedCategory! : Category
   message=''
   categoryToUpdate! : Category
   isShow: boolean = false

   ngOnInit() {
    this.getCategories();
   }

  // constructor(private _router: Router, private _categoryService: CategoryService){}
  constructor(
    private _router: Router,
    private _categoryService: CategoryService,
    private _authService: AuthService) {}

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
 onUpdatecategory() {
  this.showUpdate = false;
  this.getCategories();
}
showDetailes(id: number){
  this.isShow=true;
  this._router.navigate(['category/get-category-id',id]);
}
isManager(): boolean {
  return this._authService.isManager();
}
}

