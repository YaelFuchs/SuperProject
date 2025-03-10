import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { BranchProduct } from '../../branchProduct.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BranchProductService } from '../../branchProduct.service';
import { BranchService } from '../../../branch/branch.service';
import { ProductService } from '../../../product/product.service';
import { Branch } from '../../../branch/branch.model';
import { Product } from '../../../product/product.model';

@Component({
  selector: 'app-add-branch-product',
  templateUrl: './add-branch-product.component.html',
  styleUrl: './add-branch-product.component.scss'
})
export class AddBranchProductComponent implements OnInit {
  @Output() branchProductAdded = new EventEmitter<BranchProduct>();
  branches!: Branch[]
  products!: Product[]
  message = ''
  public addForm!: FormGroup;
  constructor(private _branchProductService: BranchProductService, private _branchService: BranchService, private _productService: ProductService) { }
  ngOnInit(): void {
    this._branchService.getBranches().subscribe({
      next: (res) => {
        console.log('res', res);
        this.branches = res;
      },
      error: (err) => {
        console.log('error', err);
      }
    })
    this._productService.getProducts().subscribe({
      next: (res) => {
        console.log('res', res);
        this.products = res;
      },
      error: (err) => {
        console.log('error', err);
      }
    })
    this.addForm = new FormGroup({
      branchId: new FormControl('', Validators.required),
      productId: new FormControl('', Validators.required),
      price: new FormControl('', [Validators.required, Validators.min(0)])
    })
  }

  addBranchProduc() {

    this._branchProductService.addBranchProduct(this.addForm.value).subscribe({
      next: (res) => {
        console.log("הסניף נוסף בהצלחה", res);
        this.branchProductAdded.emit(res);
      },
      error: (err) => {
        this.message = err;
      }
    })
  }
}
