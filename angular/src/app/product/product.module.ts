import { NgModule } from "@angular/core";
import { GetProductComponent } from "./component/get-product/get-product.component";
import { GetProductIdComponent } from "./component/get-product-id/get-product-id.component";
import { CommonModule, NgClass, NgSwitch, NgSwitchCase } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductRoutingModule } from "./product.routing.module";
import { AddProductComponent } from "./component/add-product/add-product.component";
import { UpdateProductComponent } from "./component/update-product/update-product.component";

@NgModule({
    declarations: [GetProductComponent, GetProductIdComponent, AddProductComponent, UpdateProductComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgSwitch,
        NgSwitchCase,
        ProductRoutingModule,

    ],
})
export class ProductModule{}