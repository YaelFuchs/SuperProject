import { Component ,OnInit} from '@angular/core';
import { User } from '../../user.model';
import { UserService } from '../../user.service';
import { Router } from "@angular/router";
import { AuthService } from '../../../auth/auth.service';

@Component({
  selector: 'app-personal-area',
  templateUrl: './personal-area.component.html',
  styleUrl: './personal-area.component.scss'
})
export class PersonalAreaComponent implements OnInit{
  userData!: User
  userId!:number
  constructor(private _userService: UserService, private _router: Router, private _authService: AuthService){}
  ngOnInit(): void {
    const authDataString = localStorage.getItem('authToken');
    if (authDataString) {
      try {
        const authData = JSON.parse(authDataString);
        this.userId = authData.userId || 0;
        console.log('User ID מ-localStorage:', this.userId);
      } catch (error) {
        console.error('שגיאה בפרסור authToken:', error);
        this.userId = 0;
      }
    } else {
      console.log('אין authToken ב-localStorage');
    }
    this._userService.getUserById(this.userId).subscribe({
    next: (res) => {
      console.log("data",res);
      
      this.userData = res; 
  },
  error:(err)=>{
    console.log("אין יוזר"); 
  }
})
  }
update(){
  this._router.navigate(['user/update-user']);
}
delete(){
  const id=this.userId;
  this.logout();
  this._userService.deleteUser(id).subscribe({
    next:(res)=>{
      console.log("המשתמש נמחק בהצלחה");
    }, 
    error:(err)=>{
      console.log("המשתמש לא נמחק");
    }
  })
}
logout(){
this._authService.logout();
}
}
