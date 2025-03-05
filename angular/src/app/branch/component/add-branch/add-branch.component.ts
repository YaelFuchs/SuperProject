import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Branch } from '../../branch.model';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BranchService } from '../../branch.service';

@Component({
  selector: 'app-add-branch',
  templateUrl: './add-branch.component.html',
  styleUrl: './add-branch.component.scss'
})
export class AddBranchComponent implements OnInit  {
  @Output() branchAdded = new EventEmitter<Branch>();

  message=''
  public addForm!:FormGroup;

  constructor(private _branchService: BranchService){}

  ngOnInit(): void {
    this.addForm = new FormGroup({
     name: new FormControl('', Validators.required),
     phone:new FormControl('', [Validators.required,Validators.minLength(7)]),
     address:new FormControl('', Validators.required),
     email:new FormControl('', [Validators.required,Validators.email])
    })
  }
  addBranch(){
    
    this._branchService.addBranch(this.addForm.value).subscribe({
      next: (res)=>{
        console.log("הסניף נוסף בהצלחה", res);
        this.branchAdded.emit(res);
      },
      error:(err)=>{
        this.message = err;
      }
    })
   }
}
