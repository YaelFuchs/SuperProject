import { NgModule } from "@angular/core";
import { GetCartComponent } from "./component/get-cart/get-cart.component";
import { CommonModule, NgClass, NgSwitch, NgSwitchCase } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { CartRoutingModule } from "./cart.routing.module";

@NgModule({
    declarations: [GetCartComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgSwitch,
        NgSwitchCase,
        CartRoutingModule,
        

    ],
})

export class CartModule { }