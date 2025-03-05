import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { BranchProduct, BranchProductPostModel } from "./branchProduct.model";

@Injectable({
     providedIn: 'root'
})

export class BranchProductService{
    basicUrl='https://localhost:7173/api/BranchProducts';

    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })

    
    constructor(private _httpClient: HttpClient) { }

    getBranchProducts(): Observable<BranchProduct[]>{
       return this._httpClient.get<BranchProduct[]>(this.basicUrl);
    }

    getBranchProduct(id:number): Observable<BranchProduct>{
        return this._httpClient.get<BranchProduct>(`${this.basicUrl}/${id}`);  
    }

    deleteBranchProduct(id: number): Observable<any>{
        return this._httpClient.delete<any>(`${this.basicUrl}/${id}`)
    }

    addBranchProduct(branchProduct:BranchProduct ): Observable<any> {
      return  this._httpClient.post<any>(this.basicUrl,branchProduct);
    }

    updateBranchProduct(id:number,branchProduct:BranchProductPostModel ):Observable<any> {
        return  this._httpClient.put<any>(`${this.basicUrl}/${id}`,branchProduct);
    }

    


}