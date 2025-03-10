import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Branch } from "./branch.model";

@Injectable({
    providedIn: 'root'  // ← מוודא שהשירות זמין בכל האפליקציה!
})
export class BranchService {
    basicUrl = 'https://localhost:7173/api/Branches';
    $source: Observable<number> = new Observable<number>((observer) => {
        observer.next(1);
        observer.complete();
        observer.error('error');
    })
    constructor(private _httpClient: HttpClient) { }

    getBranches(): Observable<Branch[]> {
        return this._httpClient.get<Branch[]>(this.basicUrl);
    }
    getBranchById(id: number): Observable<Branch> {
        return this._httpClient.get<Branch>(`${this.basicUrl}/${id}`);
    }
    addBranch(branch: Branch): Observable<any> {
        return this._httpClient.post<any>(this.basicUrl, branch);
    }

    updateBranch(id: number, branch: Branch): Observable<any> {
        return this._httpClient.put<Branch>(`${this.basicUrl}/${id}`, branch);
    }
    deleteBranch(id: number): Observable<any> {
        return this._httpClient.delete<any>(`${this.basicUrl}/${id}`);
    }

}