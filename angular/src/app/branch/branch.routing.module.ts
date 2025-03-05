import { Route, RouterModule } from "@angular/router";
import { GetBranchesComponent } from "./component/get-branches/get-branches.component";
import { GetBranchIdComponent } from "./component/get-branch-id/get-branch-id.component";
import { NgModule } from "@angular/core";
import { AddBranchComponent } from "./component/add-branch/add-branch.component";
import { UpdateBranchComponent } from "./component/update-branch/update-branch.component";

const routes: Route[]=[
    {path:'',component : GetBranchesComponent},
    {path:'get-branch-id/:id',component:GetBranchIdComponent},
    {path:'add-branch',component:AddBranchComponent},
    {path:'update-branch', component:UpdateBranchComponent}
]
@NgModule({
    imports:[
        RouterModule.forChild(routes)
    ],
})
export class BranchRoutingMoudle{}


