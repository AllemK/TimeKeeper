import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { IEmployee } from '../models/employee';

@Component({
  selector: 'employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})

export class EmployeeComponent implements OnInit {

  pageTitle = 'Employee Detail';
  employee: IEmployee

  constructor(private route: ActivatedRoute,
              private router: Router) {  }

  ngOnInit() {
    let id = +this.route.snapshot.paramMap.get('id');
    this.pageTitle += `: ${id}`;
  }

  onBack(): void {
    this.router.navigate(['/employees']);
  }
}
