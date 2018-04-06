import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { IEmployee, IPagination } from '../models/employee';
import { EmployeeService } from '../shared/employee.service';

@Component({
  selector: 'employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})

export class EmployeesComponent implements OnInit {
  pageTitle: string = "Employee List";

  employees: IEmployee[];
  pagination: IPagination;
  
  constructor(private employeeService: EmployeeService) { }

  pages(): number[] {
    let arr = new Array(this.pagination.pageCount).fill(0);
    for(let i in arr) arr[i] = +i+1;
    return arr;
  }

  btnClass(page: number){
    return (page == this.pagination.currentPage) ? 'btn btn-warning' : 'btn btn-success';
  }

  goto(page:number): void{
    this.pagination.currentPage = page;
    this.employeeService.getEmployees(this.pagination)
      .subscribe(response => {
        this.employees = response.body;
        this.pagination = JSON.parse(response.headers.get('Pagination'));
      });
  }

  ngOnInit() { 
    this.pagination = { currentPage:1, pageCount:0, pageSize:0, itemCount:0 };
    this.goto(1);
  }

  Age(startDate: Date): number {
    let start: Date = new Date(startDate);
    let diff: number = Math.round((new Date().getTime() - start.getTime()) / (365.25 * 24 * 60 * 60 * 10)) / 100;
    return Math.min(85, diff);
  }
}