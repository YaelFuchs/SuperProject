import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { PostProduct, Product } from "./product.model";

@Injectable({
    providedIn: 'root'
})

export class ProductService {
    basicUrl = 'https://localhost:7173/api/Products';
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })

    constructor(private _httpClient: HttpClient) { }

    getProducts(): Observable<Product[]> {
        return this._httpClient.get<Product[]>(this.basicUrl);
    }
    getProductById(id: number): Observable<Product> {
        return this._httpClient.get<Product>(`${this.basicUrl}/${id}`);

    }
    deleteProduct(id: number): Observable<any> {
        return this._httpClient.delete<any>(`${this.basicUrl}/${id}`)
    }
    addProduct(product: FormData): Observable<any> {
        return this._httpClient.post<any>(this.basicUrl, product);
    }
    updateProduct(id: number, product: PostProduct): Observable<any> {
        return this._httpClient.put<any>(`${this.basicUrl}/${id}`, product);
    }
}