import { Component, OnInit } from '@angular/core';
import { HeaderService } from '../template/header/header.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  constructor(private headerService: HeaderService) {
    this.headerService.setHeaderData('Dashboard', 'dashboard', '/dashboard');
  }

  ngOnInit(): void {
  }

}
