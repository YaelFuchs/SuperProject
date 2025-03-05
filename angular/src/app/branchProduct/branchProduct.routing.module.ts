import { NgModule } from "@angular/core";
import { GetBranchProductsComponent } from "./component/get-branch-products/get-branch-products.component";
import { GetbranchProductIdComponent } from "./component/getbranch-product-id/getbranch-product-id.component";
import { Route, RouterModule } from "@angular/router";
import { AddBranchProductComponent } from "./component/add-branch-product/add-branch-product.component";
import { UpdateBranchProductComponent } from "./component/update-branch-product/update-branch-product.component";

const routes :Route [] = [
    {path: "", component: GetBranchProductsComponent},
    { path: 'getbranch-product-id/:id', component: GetbranchProductIdComponent},
    { path: 'add-branch-product', component: AddBranchProductComponent},
    { path: 'update-branch-product', component: UpdateBranchProductComponent}

]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ]
})
export class BranchProductRoutingModule{}