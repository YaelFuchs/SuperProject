import { CommonModule, NgClass, NgSwitchCase } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { GetBranchesComponent } from "./component/get-branches/get-branches.component";
import { GetBranchIdComponent } from "./component/get-branch-id/get-branch-id.component";
import { BranchRoutingMoudle } from "./branch.routing.module";
import { AddBranchComponent } from "./component/add-branch/add-branch.component";
import { UpdateBranchComponent } from "./component/update-branch/update-branch.component";

@NgModule({
    declarations:[GetBranchesComponent,GetBranchIdComponent ,AddBranchComponent ,UpdateBranchComponent 
    ],
    imports:[ 
        CommonModule,
        ReactiveFormsModule,
        NgClass,
        FormsModule,
        NgClass,
        NgSwitchCase,
        BranchRoutingMoudle
    ],
})
export class BranchModule{}