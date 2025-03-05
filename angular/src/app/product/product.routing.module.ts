import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { GetProductComponent } from "./component/get-product/get-product.component";
import { GetProductIdComponent } from "./component/get-product-id/get-product-id.component";

const routes : Route[] = [
    {path: "", component: GetProductComponent},
    // { path: 'update-product', component: UpdateProductComponent},
    { path: 'get-product-id/:id', component: GetProductIdComponent}
]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ]
})
export class ProductRoutingModule{}