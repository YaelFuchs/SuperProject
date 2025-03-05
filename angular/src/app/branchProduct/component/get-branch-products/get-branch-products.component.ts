import { Component, OnInit } from '@angular/core';
import { BranchProduct } from '../../branchProduct.model';
import { BranchProductService } from '../../branchProduct.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-branch-products',
  templateUrl: './get-branch-products.component.html',
  styleUrl: './get-branch-products.component.scss'
})
export class GetBranchProductsComponent implements OnInit{
  branchProducts!: BranchProduct[];
  showAdd = false
  showUpdate = false
  selectedBranchProduct! : BranchProduct
  message=''
  branchProductToUpdate! : BranchProduct
  isShow: boolean = false

  constructor(private _branchProductService: BranchProductService,private _router:Router , private _authService: AuthService){}

  ngOnInit() {
   this.getBranchProducts();
    }

   getBranchProducts() {
    this._branchProductService.getBranchProducts().subscribe({
      next:(res)=>{
        console.log('res',res);
        this.branchProducts=res;
      },
      error:(err)=>{
        console.log('err',err);       
      }
    })
   }

   showDetails(id:number){
    this.isShow=true;
    this._router.navigate(['branchProduct/getbranch-product-id',id]);
   }

   isAdmin(): boolean {
    return this._authService.isAdmin();
  }

  isManager(): boolean {
    return this._authService.isManager();
  }

  onBranchProductAdded(branchProduct:BranchProduct):void{
    this.showAdd=false;
    this.getBranchProducts();
  }
  
  update(branchProduct:BranchProduct):void{
    this.branchProductToUpdate=branchProduct;
    this.showUpdate=true;
  }

  onUpdateBranchProduct():void{    
    this.showUpdate=false;
    this.getBranchProducts();
  }


}
