import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup,Validators } from '@angular/forms';
import { UserService } from '../../user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent implements OnInit {
public addForm !: FormGroup;
constructor(private _userService: UserService){}
ngOnInit() {
  this.addForm = new FormGroup({
      UserName: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required, Validators.minLength(8)])
  });
}

   signUp(){
    console.log("פה");
    console.log(this.addForm.value);
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
