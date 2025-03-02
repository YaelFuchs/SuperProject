import { Injectable } from "@angular/core";
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs'
import { User } from "./user.model";
@Injectable({
    providedIn: 'root'
})
export class UserService{
    basicUrl = 'https://localhost:7173/api/User';
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })
    constructor(private _httpClient: HttpClient){}

    signUp(user: User):Observable<any>{
        return this._httpClient.post<User>(this.basicUrl, user);
    }

}