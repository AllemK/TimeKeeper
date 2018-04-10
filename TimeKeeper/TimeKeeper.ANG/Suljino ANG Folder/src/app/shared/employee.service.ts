import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { IEmployee, IPagination } from "../models/employee";
import { Observable } from "rxjs/Observable";

@Injectable()

export class EmployeeService {

    private apiUrl = 'http://localhost:9000/api/employees';

    constructor (private http: HttpClient) { }

    getEmployees(page: IPagination): Observable<HttpResponse<IEmployee[]>> {
        let head = new HttpHeaders({'Pagination': JSON.stringify(page)});
        return this.http.get<IEmployee[]>(this.apiUrl, { headers: head, observe:'response' });
    }
}