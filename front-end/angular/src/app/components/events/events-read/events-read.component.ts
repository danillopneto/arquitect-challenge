import { EventData } from './../../../models/events/eventdata.model';
import { EventReadService } from './events-read.service';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { EventsReadDataSource } from './events-read-datasource';

@Component({
  selector: 'app-events-read',
  templateUrl: './events-read.component.html',
  styleUrls: ['./events-read.component.css']
})
export class EventsReadComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<EventData>;
  dataSource!: EventsReadDataSource;

  displayedColumns = ['tag', 'timestamp', 'valor', 'status'];

  constructor(private eventReadService: EventReadService){
    this.dataSource = new EventsReadDataSource([]);
  }

  ngOnInit() {
    this.loadEvents();
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  loadEvents() {
    this.eventReadService.getAll().subscribe(events => {
      this.setTableData(events);
    });
  }

  setTableData(events: EventData[]): void {
    this.dataSource = new EventsReadDataSource(events);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }
}
