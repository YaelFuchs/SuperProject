import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class PayPalService {
    basicUrl='https://localhost:7173/api/Payment';
        $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })
    approvalUrl!: string;
    constructor(private http: HttpClient) { }


    createPayment() {
        this.http.get<{ url: string }>(this.basicUrl).subscribe(
          response => {
            this.approvalUrl = response.url;
            window.location.href = this.approvalUrl; // הפניה ל-PayPal
          },
          error => {
            console.error('שגיאה ביצירת תשלום', error);
          }
        );
      }
}