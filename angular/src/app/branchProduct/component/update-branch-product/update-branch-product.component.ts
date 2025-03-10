import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BranchProduct, BranchProductPostModel } from '../../branchProduct.model';
import { BranchProductService } from '../../branchProduct.service';
import { BranchService } from '../../../branch/branch.service';
import { ProductService } from '../../../product/product.service';
import { Product } from '../../../product/product.model';
import { Branch } from '../../../branch/branch.model';

@Component({
  selector: 'app-update-branch-product',
  templateUrl: './update-branch-product.component.html',
  styleUrl: './update-branch-product.component.scss'
})
export class UpdateBranchProductComponent implements OnInit {
  @Output() updateBranchProduct = new EventEmitter<BranchProduct>()
  @Input() public branchProductUpdate!: BranchProduct
  products!: Product[]
  branches!: Branch[]
  message = ''
  public branchProduct!: BranchProductPostModel

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
    this.branchProduct = {
      id: this.branchProductUpdate.id,
      branchId: this.branchProductUpdate.branch.id,
      productId: this.branchProductUpdate.product.id,
      price: this.branchProductUpdate.price
    }
  }

  saveChanges() {
    this._branchProductService.updateBranchProduct(this.branchProduct.id, this.branchProduct).subscribe({
      next: (res) => {
        console.log("המוצר סניף עודכן בהצלחה", res);
        this.updateBranchProduct.emit(res);
      },
      error: (err) => {
        this.message = err
        console.log("שגיאה בעדכון המוצר סניף:", this.message);

      }
    })
  }


}
