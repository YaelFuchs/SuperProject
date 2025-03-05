import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from './auth.service';

@Component({
  selector: 'app-auth',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,RouterModule],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.scss'
})
export class AuthComponent implements OnInit {

    addForm !:FormGroup
    constructor(private _authService : AuthService){}
    ngOnInit() {
     this.addForm = new FormGroup({
         UserName: new FormControl('', Validators.required),
         Password: new FormControl('', [Validators.required, Validators.minLength(4)])
     });
   }
     login(){
       this._authService.login(this.addForm.value)
     }
   }
   

