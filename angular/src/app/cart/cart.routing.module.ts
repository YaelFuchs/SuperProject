import { NgModule } from "@angular/core";
import { GetCartComponent } from "./component/get-cart/get-cart.component";
import { Route, RouterModule } from "@angular/router";

const routes: Route[] = [
    { path: '', component: GetCartComponent },


]

@NgModule({
    imports: [
        RouterModule.forChild(routes)//אלו הנתיבים של המודול הזה, תשתמש בהם כשתצטרך
    ],
})
export class CartRoutingModule { }