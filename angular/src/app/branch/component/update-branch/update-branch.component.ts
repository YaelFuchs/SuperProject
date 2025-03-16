import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Branch } from '../../branch.model';
import { BranchService } from '../../branch.service';

@Component({
  selector: 'app-update-branch',
  templateUrl: './update-branch.component.html',
  styleUrl: './update-branch.component.scss'
})
export class UpdateBranchComponent implements OnInit {
  @Output() updateBranch = new EventEmitter<Branch>();
  @Input() public branchUpdate!: Branch
  message = ''
  public branch!: Branch

  constructor(private _branchService: BranchService) { }
  ngOnInit(): void {
    this.branch = {
      id: this.branchUpdate.id,
      name: this.branchUpdate.name,
      phone: this.branchUpdate.phone,
      address: this.branchUpdate.address,
      email: this.branchUpdate.email,
      shippingCost: this.branchUpdate.shippingCost
    }
  }
  saveChanges() {
    this._branchService.updateBranch(this.branch.id, this.branch).subscribe({
      next: (res) => {
        console.log("הסניף עודכן בהצלחה", res);
        this.updateBranch.emit(res);
      },
      error: (err) => {
        this.message = err
        console.log("שגיאה בעדכון הסניף:", this.message);

      }
    })
  }
}
