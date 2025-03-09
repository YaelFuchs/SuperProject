import { HttpClient} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CartItem, PostCart } from "./cart.model";
import { PostProduct } from "../product/product.model";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class CartService {
    basicUrl = 'https://localhost:7173/api/ShoppingCart';
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })

    constructor(private _httpClient: HttpClient) {}

    addProduct(userId: number,product: PostCart ): Observable<any> {
      return this._httpClient.post<any>( `${this.basicUrl}/addToCart/${userId}`, product );  
  }

    addCart(userId: number): Observable<any>{
      return this._httpClient.post<any>(this.basicUrl,userId);
    }

    removeProduct(userId: number,product: PostProduct): Observable<any>{
      return this._httpClient.put<any>(`${this.basicUrl}/${userId}`, product);
    }

    clearCart(userId: number): Observable<any>{
      return this._httpClient.delete<any>(`${this.basicUrl}/${userId}`);
    }

    getCartByUserId(userId: number):Observable<CartItem[]>{
        return this._httpClient.get<CartItem[]>(`${this.basicUrl}/${userId}`);
    }
    orderCart(userId: number){
      
    }
}