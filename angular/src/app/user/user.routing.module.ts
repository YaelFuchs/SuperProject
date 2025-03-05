import { Route, RouterModule } from "@angular/router";
import { NgModule } from "@angular/core";
import { SignUpComponent } from "./component/sign-up/sign-up.component";
import { UpdateUserComponent } from "./component/update-user/update-user.component";

const routes: Route[]=[
    {path: '', component:SignUpComponent},
    {path: 'update-user', component: UpdateUserComponent}
]
@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
})
export class UserRoutingModule{};