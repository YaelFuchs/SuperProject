import { Component, OnInit } from '@angular/core';
import { Branch } from '../../branch.model';
import { BranchService } from '../../branch.service';
import { Router } from '@angular/router';
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-get-branches',
  templateUrl: './get-branches.component.html',
  styleUrl: './get-branches.component.scss'
})
export class GetBranchesComponent implements OnInit {
  branches!: Branch[]
  showAdd = false
  showUpdate = false
  selectedBranch!: Branch
  message = ''
  branchToUpdate!: Branch
  isShow: boolean = false

  constructor(private _branchService: BranchService, private _router: Router, private _authService: AuthService) { }

  ngOnInit() {
    this.getBranches();
  }

  getBranches() {
    this._branchService.getBranches().subscribe({
      next: (res) => {
        console.log('res', res);
        this.branches = res;
      },
      error: (err) => {
        console.log('error', err);

      }
    })
  }
  showDetailes(id: number) {
    this.isShow = true;
    this._router.navigate(['branch/get-branch-id', id]);
  }
  isAdmin(): boolean {
    return this._authService.isAdmin();
  }
  isManager(): boolean {
    return this._authService.isManager();
  }
  onBrabchAdded(brabch: Branch): void {
    this.showAdd = false;
    this.getBranches();
  }
  update(brabch: Branch): void {
    this.branchToUpdate = brabch;
    this.showUpdate = true;
  }
  onUpdateBrabch(): void {
    this.showUpdate = false;
    this.getBranches();
  }


}
