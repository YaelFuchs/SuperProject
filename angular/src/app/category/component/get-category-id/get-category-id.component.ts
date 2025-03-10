import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../category.service';
import { Category } from '../../category.model';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-category-id',
  templateUrl: './get-category-id.component.html',
  styleUrl: './get-category-id.component.scss'
})
export class GetCategoryIdComponent implements OnInit {
  message = ''
  id = 0
  categoryDetails!: Category
  constructor(private _categoryService: CategoryService, private _router: ActivatedRoute, private _r: Router,
    private _authService: AuthService) { }
  ngOnInit(): void {
    this._router.params.subscribe((param) => {
      this.id = +param['id'];
      this._categoryService.getCategoryById(this.id).subscribe({
        next: (res) => {
          this.categoryDetails = res;
        },
        error: (err) => {
          this.message = err;
        }
      })
    });
  }

  public delete(id: number) {
    this._categoryService.deleteCategory(id).subscribe({
      next: (res) => {
        console.log("delete: ", res);
        this._r.navigate(['category/'])
      },
      error: (err) => {
        this.message = err;
      }
    })
  }

  goBack() {
    this._r.navigate(['category/'])
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isManager(): boolean {
    return this._authService.isManager();
  }


}
