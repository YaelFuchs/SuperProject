import { Route, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { SignUpComponent } from "./component/sign-up/sign-up.component";

const routes: Route[]=[
    {path: '', component:SignUpComponent}
]
@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
})
export class UserRoutingModule{};