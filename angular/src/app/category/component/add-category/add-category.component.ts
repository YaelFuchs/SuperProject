import { Component,EventEmitter, OnInit, Output  } from '@angular/core';
import { FormControl, FormGroup, Validators} from '@angular/forms'

import { CategoryService } from '../../category.service';
import { Category } from '../../category.model';


@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrl: './add-category.component.scss'
})
export class AddCategoryComponent implements OnInit {
  @Output()categoryAdded = new EventEmitter<Category>(); 
  message=''
 public addForm! : FormGroup;

 constructor(private _categoryService: CategoryService){}

 ngOnInit(): void {
   this.addForm = new FormGroup({
    name: new FormControl('', Validators.required)
   })
 }
 addCategory(){
  this._categoryService.addCategoryServise(this.addForm.value).subscribe({
    next: (res)=>{
      console.log("הקטגוריה נוספה בהצלחה", res);
      this.categoryAdded.emit(res);
    },
    error:(err)=>{
      this.message = err;
    }
  })
 }

}
