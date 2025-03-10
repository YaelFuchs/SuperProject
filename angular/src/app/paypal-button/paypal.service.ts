
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {
  basicUrl = 'https://localhost:7173/api/Checkout';
  $source: Observable<number> = new Observable<number>((observer) => {
    observer.next(1) //on succeed
    observer.complete(); //on ending
    observer.error('error'); //on error
  })

  constructor(private http: HttpClient) { }

  createOrder(orderInput: any): Observable<any> {
    return this.http.post(`${this.basicUrl}/orders`, orderInput);
  }

  captureOrder(orderId: string): Observable<any> {
    return this.http.post(`${this.basicUrl}/orders/${orderId}/capture`, {});
  }
}

