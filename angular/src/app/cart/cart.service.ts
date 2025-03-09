import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CartItem } from "./cart.model";
import { Product } from "../product/product.model";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class CartService {
    basicUrl = 'https://localhost:7173/api/ShoppingCart';
    userId=0
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })

    constructor(private _httpClient: HttpClient) {}
    getUserId(){
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
    }
    addProduct(product:Product):Observable<any>{
        return this._httpClient.post<Observable<any>(`${this.basicUrl}/{addToCart}`,this.userId,product);

    }
    addCart(){}
    removeProduct(){}
    clearCart(){}

    getCartItemByUserId():Observable<CartItem[]>{
        return this._httpClient.get<CartItem[]>(this.basicUrl);
    }
}