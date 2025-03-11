import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { GetProductComponent } from "./component/get-product/get-product.component";
import { GetProductIdComponent } from "./component/get-product-id/get-product-id.component";
import { AddProductComponent } from "./component/add-product/add-product.component";
import { UpdateProductComponent } from "./component/update-product/update-product.component";

const routes: Route[] = [
    { path: "", component: GetProductComponent },
    { path: 'get-product/:category', component: GetProductComponent },
    { path: 'get-product/search/:word', component: GetProductComponent },
    { path: 'get-product-id/:id', component: GetProductIdComponent },
    { path: 'add-product', component: AddProductComponent },
    { path: 'update-product', component: UpdateProductComponent },

]

@NgModule({
    imports: [
        RouterModule.forChild(routes),
    ]
})
export class ProductRoutingModule { }