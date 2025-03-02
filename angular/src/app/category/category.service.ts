import { Injectable } from "@angular/core";  //דקורטור שמסמן שהמחלקה היא Service (שירות) שניתן להזריק (Inject) אותו לקומפוננטות אחרות.
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";  //חלק מספריית RxJS (Reactive Extensions for JavaScript), שמאפשר עבודה עם נתונים אסינכרוניים.
import { Category } from "./category.model";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class CategoryService {
    basicUrl = 'https://localhost:7173/api/Categories';
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1) //on succeed
        observer.complete(); //on ending
        observer.error('error'); //on error
    })

    constructor(private _httpClient: HttpClient) {

    }
    getCategoriesFromServer(): Observable<Category[]> {
        return this._httpClient.get<Category[]>(this.basicUrl);
    }

    addCategoryServise(category: Category): Observable<Category>{
        return  this._httpClient.post<Category>(this.basicUrl, category);
    }

    updateCategory(id: number, category: Category): Observable<any>{
        return this._httpClient.put<Category>(`${this.basicUrl}/${id}`, category);
    }

    getCategoryById(id: number):Observable<Category>{
      return this._httpClient.get<Category>(`${this.basicUrl}/${id}`)
    }
    deleteCategory(id: number):Observable<any>{
       return this._httpClient.delete<number>(`${this.basicUrl}/${id}`)
    }
}