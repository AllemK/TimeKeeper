import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";

@Injectable()

export class DataService {

    private apiUrl = 'http://localhost:54283/api/';

    constructor (private http: HttpClient) { }

    list(dataSet:string): Observable<any[]> {
        let url = this.apiUrl + dataSet;
        return this.http.get<any[]>(url);
    }

    insert(dataSet:string, data:any): Observable<any> {
        let url = this.apiUrl + dataSet;
        return this.http.post(url, data);
    }

    update(dataSet:string, data:any, id:any): Observable<any> {
        let url = this.apiUrl + dataSet + '/' + id;
        return this.http.put(url, data);
    }

    delete(dataSet:string, id:any): Observable<any> {
        let url = this.apiUrl + dataSet + '/' + id;
        return this.http.delete(url);
    }
}