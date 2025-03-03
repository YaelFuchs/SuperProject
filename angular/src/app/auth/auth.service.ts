import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Auth } from "./auth.model";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class AuthService{
    basicUrl='https://localhost:7173/api/Auth';
    $source:Observable<number>=new Observable<number>((observe)=>{
        observe.next(1);
        observe.complete();
        observe.error('error');
    })
    constructor( private _httpClient:HttpClient){}
    isAuthtnticated$= new BehaviorSubject<boolean>(!!localStorage.getItem("authToken"));

    get isAuthenticated():boolean{
        return this.isAuthtnticated$.getValue();
    }
     login(auth:Auth):void{
        this._httpClient.post<{token : string}>(this.basicUrl,auth).subscribe({
            next:(res)=>{
                console.log(res);
              try{
                localStorage.setItem('authToken',JSON.stringify({token : res.token}))
                this.isAuthtnticated$.next(true);
              } catch(error){
                  console.log("שגיאה בשמירה",error);                 
              }
            },
            error: (err)=>{               
               alert("שגיאה בהתחברות"+err.error);              
            }
        })
     }

    // login(auth:Auth):Observable<{token : string}>{
    //     const token= this._httpClient.post<{token : string}>(this.basicUrl,auth);
    //     localStorage.setItem('authToken',JSON.stringify(token));
    //     return token;
    // }
    
    getToken(): string | null{
        return localStorage.getItem('authToken');
    }
    logout(): void {
        localStorage.removeItem('authToken')
    }
    // isLoggedIn() :boolean {
    //     return !! this.getToken();
    // }
}

