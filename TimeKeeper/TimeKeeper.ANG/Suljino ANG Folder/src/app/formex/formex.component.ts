import { Component } from '@angular/core';

@Component({
  selector: 'formex',
  templateUrl: './formex.component.html',
  styleUrls: ['./formex.component.css']
})
export class FormexComponent {

  pageTitle = 'Form elements example';
  year: string[] = ['jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec'];
  monty: string = 'apr';

  yearMap: boolean[] = [false, false, false, false, false, false, false, false, false, false, false, false];
  choice: string = '<empty>';

  updateChecked(month: string, event): void {
    this.yearMap[this.year.indexOf(month)] = event.target.checked;
    this.choice = '';
    for(let i in this.yearMap){
        if (this.yearMap[i]) this.choice += this.year[i] + ' ';
    }
  }
}
