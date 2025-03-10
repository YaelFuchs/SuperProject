import { Component, OnInit } from '@angular/core';
import { Branch } from '../../branch.model';
import { BranchService } from '../../branch.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-branch-id',
  templateUrl: './get-branch-id.component.html',
  styleUrl: './get-branch-id.component.scss'
})
export class GetBranchIdComponent implements OnInit {
  message = ''
  id = 0
  branchDetails!: Branch

  constructor(private _branchService: BranchService, private _activatedRoute: ActivatedRoute,
    private _router: Router, private _authService: AuthService) { }
  ngOnInit(): void {
    this._activatedRoute.params.subscribe((param) => {
      this.id = +param['id'];
      this._branchService.getBranchById(this.id).subscribe({
        next: (res) => {
          this.branchDetails = res
        },
        error: (err) => {
          this.message = err
          console.log("error", this.message);
        }
      })
    })
  }
  public delete(id: number) {
    this._branchService.deleteBranch(id).subscribe({
      next: (res) => {
        console.log("res", res);
        this._router.navigate(['branch/'])
      },
      error: (err) => {
        this.message = err
        console.log("error", this.message);

      }
    })
  }
  goBack() {
    this._router.navigate(['branch/'])
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isManager(): boolean {
    return this._authService.isManager();
  }

}
