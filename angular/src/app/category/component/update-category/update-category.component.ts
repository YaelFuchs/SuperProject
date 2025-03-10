import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Category } from '../../category.model';
import { CategoryService } from '../../category.service';

@Component({
  selector: 'app-update-category',
  templateUrl: './update-category.component.html',
  styleUrl: './update-category.component.scss'
})
export class UpdateCategoryComponent implements OnInit {
  @Output() updatecategory = new EventEmitter<Category>();
  @Input() public categoryUpdate!: Category
  message = '';
  public category!: Category

  constructor(private _categoryService: CategoryService) { }

  ngOnInit(): void {

    this.category = {
      id: this.categoryUpdate.id,
      name: this.categoryUpdate.name
    }
  }
  saveChanges() {
    console.log("id:", this.category.id);
    this._categoryService.updateCategory(this.category.id, this.category).subscribe({
      next: (res) => {
        console.log("העדכון עבר בהצלחה", res);
        this.updatecategory.emit(res);
      },
      error: (err) => {
        this.message = err;
        console.log("שגיאה בעדכון!");

      }
    })
  }
}
