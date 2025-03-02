import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup,Validators } from '@angular/forms';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent implements OnInit {
  public addForm!: FormGroup
constructor(private _userService: UserService){}
  ngOnInit(): void {
    this.addForm = new FormGroup({
      userName:  new FormControl('',Validators.required),
      email: new FormControl('',[Validators.required,Validators.email]),
      password: new FormControl('',[Validators.required, Validators.minLength(8)])
    })
   }

   signUp(){
    this._userService.signUp(this.addForm.value).subscribe({
      next:(res)=>{
        console.log(res);
        
      },
      error:(err)=>{
        console.log(err);
        
      }
    })
   }

}
