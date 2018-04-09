import { Component, OnInit } from '@angular/core';
import { DataService } from '../shared/data.service';
import { ITeam } from '../models/team';

@Component({
  selector: 'teams',
  templateUrl: './teams.component.html',
  styleUrls: ['./teams.component.css']
})
export class TeamsComponent implements OnInit {

  constructor(private dataService: DataService) { }

  pageTitle: string = 'Team List';
  teams: ITeam[];

  ngOnInit(): void {
    this.dataService.list('teams').subscribe(data => { this.teams = data; })
  }

}
