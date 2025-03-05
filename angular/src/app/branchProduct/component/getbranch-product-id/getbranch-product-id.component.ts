import { Component, OnInit } from '@angular/core';
import { BranchProduct } from '../../branchProduct.model';
import { BranchProductService } from '../../branchProduct.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-getbranch-product-id',
  templateUrl: './getbranch-product-id.component.html',
  styleUrl: './getbranch-product-id.component.scss'
})
export class GetbranchProductIdComponent implements OnInit{
  message=''
  id=0
  branchProductDetails!:BranchProduct

  constructor(private _branchProductService:BranchProductService,private _activatedRoute:ActivatedRoute,
    private _router:Router , private _authService: AuthService ){}

    ngOnInit(): void {
      this._activatedRoute.params.subscribe((param)=>{        
        this.id=+param['id'];
        this._branchProductService.getBranchProduct(this.id).subscribe({
          next:(res)=>{
            this.branchProductDetails=res
          },
          error:(err)=>{
            this.message=err
            console.log("error",this.message);          
          }
        })
      })
    }

    public delete(id:number){
      this._branchProductService.deleteBranchProduct(id).subscribe({
        next:(res)=>{
          console.log("res",res);
          this._router.navigate(['branchProduct/'])
        },
        error:(err)=>{
          this.message=err
          console.log("error",this.message);
          
        }
      })
    }
    goBack(){
      this._router.navigate(['branchProduct/'])
    }
    isAdmin(): boolean {
      return this._authService.isAdmin();
    }
    isManager(): boolean {
      return this._authService.isManager();
    }
  
}
