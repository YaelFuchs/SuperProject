import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../../category.service';
import { Category } from '../../category.model';

@Component({
  selector: 'app-get-category-id',
  templateUrl: './get-category-id.component.html',
  styleUrl: './get-category-id.component.scss'
})
export class GetCategoryIdComponent implements OnInit{
  message=''
  id=0
  categoryDetails!: Category
  ngOnInit(): void {
    this._router.params.subscribe((param)=>{
      this.id = param['id'];
      this._categoryService.getCategoryById(this.id).subscribe({
        next:(res)=>{
          this.categoryDetails = res;
        },
        error:(err)=>{
          this.message = err;
        }
      })
    });
  }
  constructor(private _router: ActivatedRoute, private _categoryService: CategoryService){}

  
  

}
