import { NgModule } from "@angular/core";
import { CommonModule, NgClass, NgSwitch, NgSwitchCase } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PaypalButtonComponent } from "./component/paypal-button/paypal-button.component";
import { PayPalRoutingModule } from "./paypal.routing.module";

@NgModule({
    declarations: [PaypalButtonComponent],
    imports: [
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgSwitch,
        NgSwitchCase,
        PayPalRoutingModule,

    ],
})

export class PayPalModule { }