import { Component, OnInit } from '@angular/core';
import { IEmployee, IPagination } from '../models/employee';
import { DataService } from '../shared/data.service';
import { ICustomer } from './customer';

@Component({
    selector: 'customers',
    templateUrl: 'customers.component.html',
    styleUrls: ['customers.component.css']
})

export class CustomersComponent implements OnInit {

    constructor(private dataService: DataService) { }

    pageTitle: string = 'Customer List';
    showImage: boolean = false;
    imagePath = 'assets/images/';
    customers: ICustomer[];

    ngOnInit(): void {
        this.dataService.list('customers').subscribe(response  => { this.customers = response; })
    }    
}