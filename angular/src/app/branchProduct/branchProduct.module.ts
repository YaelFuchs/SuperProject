import { NgModule } from "@angular/core";
import { GetBranchProductsComponent } from "./component/get-branch-products/get-branch-products.component";
import { GetbranchProductIdComponent } from "./component/getbranch-product-id/getbranch-product-id.component";
import { AddBranchProductComponent } from "./component/add-branch-product/add-branch-product.component";
import { UpdateBranchProductComponent } from "./component/update-branch-product/update-branch-product.component";
import { CommonModule, NgClass, NgSwitch, NgSwitchCase } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BranchProductRoutingModule } from "./branchProduct.routing.module";

@NgModule({
    declarations:[GetBranchProductsComponent,GetbranchProductIdComponent,AddBranchProductComponent,UpdateBranchProductComponent],
    imports:[
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgSwitch,
        NgSwitchCase,
        BranchProductRoutingModule
    ],
})
export class BranchProductModule{}