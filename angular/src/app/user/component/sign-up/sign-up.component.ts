import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup,Validators } from '@angular/forms';
import { UserService } from '../../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss'
})
export class SignUpComponent implements OnInit {
public addForm !: FormGroup;
constructor(private _userService: UserService, private _router: Router){}
ngOnInit() {
  this.addForm = new FormGroup({
      UserName: new FormControl('', Validators.required),
      Email: new FormControl('', [Validators.required, Validators.email]),
      Password: new FormControl('', [Validators.required, Validators.minLength(8)])
  });
}

   signUp(){
    console.log(this.addForm.value);
    this._userService.signUp(this.addForm.value).subscribe({
      next:(res)=>{        
        this._router.navigate(['/login'])
      },
      error:(err)=>{
        console.log(err);
        
      }
    })
   }

}
