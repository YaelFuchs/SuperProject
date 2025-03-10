import { NgModule } from "@angular/core";
import { Route, RouterModule } from "@angular/router";
import { PaypalButtonComponent } from "./component/paypal-button/paypal-button.component";

const routes: Route[] = [
    { path: '', component: PaypalButtonComponent },
    

]

@NgModule({
    imports: [
        RouterModule.forChild(routes)//אלו הנתיבים של המודול הזה, תשתמש בהם כשתצטרך
    ],
})
export class PayPalRoutingModule { }