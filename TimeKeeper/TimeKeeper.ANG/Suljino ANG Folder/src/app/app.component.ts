import { Component } from '@angular/core';
import { EmployeeService } from './shared/employee.service';
import { DataService } from './shared/data.service';

@Component({
  selector: 'root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [
    EmployeeService, 
    DataService
  ]
})
export class AppComponent {
  title = 'TimeKeeper';
}
