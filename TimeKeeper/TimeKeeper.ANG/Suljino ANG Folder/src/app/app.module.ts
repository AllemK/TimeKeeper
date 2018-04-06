import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from "@angular/router";

import { AppComponent } from './app.component';
import { WelcomeComponent } from './home/welcome.component';
import { EmployeesComponent } from './employees/employees.component';
import { EmployeeComponent } from './employees/employee.component';
import { AgesComponent } from './shared/ages.component';
import { FormexComponent } from './formex/formex.component';
import { CustomersComponent } from './customers/customers.component';
import { TeamsComponent } from './teams/teams.component';
import { EmpGuardService } from './employees/emp-guard-service';

@NgModule({
  declarations: [
    AppComponent,
    WelcomeComponent,
    EmployeesComponent,
    EmployeeComponent,
    AgesComponent,
    FormexComponent,
    CustomersComponent,
    TeamsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path:"home", component: WelcomeComponent },
      { path:"team", component: TeamsComponent },
      { path:"empl", component: EmployeesComponent },
      { path:"empl/:id", canActivate:[EmpGuardService], component: EmployeeComponent },
      { path:"cust", component: CustomersComponent },
      { path:"", redirectTo:"home", pathMatch:"full"},
      {path:"**", redirectTo:"home", pathMatch:"full"}
    ])
  ],
  providers: [ ],
  bootstrap: [AppComponent]
})

export class AppModule { }
