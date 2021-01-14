import { EventData } from './../../../models/events/eventdata.model';
import { EventReadService } from './events-read.service';
import { AfterViewInit, Component, OnInit, ViewChild, OnDestroy } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { EventsReadDataSource } from './events-read-datasource';

@Component({
  selector: 'app-events-read',
  templateUrl: './events-read.component.html',
  styleUrls: ['./events-read.component.css']
})
export class EventsReadComponent implements AfterViewInit, OnInit, OnDestroy {
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatTable) table!: MatTable<EventData>;
  dataSource!: EventsReadDataSource;

  autoUpdate = true;
  updateRunning: any;

  displayedColumns = ['tag', 'timestamp', 'valor', 'status'];

  constructor(private eventReadService: EventReadService) {
    this.dataSource = new EventsReadDataSource([]);
  }

  ngOnInit() {
    this.loadEvents();
    this.updateSeconds();
  }

  ngOnDestroy() {
    clearInterval(this.updateRunning);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  updateSeconds() {
    if (this.autoUpdate) {
      var _this = this;
      this.updateRunning = setInterval(function () {
        _this.getNewestEvents();
      }, 5000);
    } else {
      clearInterval(this.updateRunning);
    }
  }

  getNewestEvents() {
    var lastId = this.dataSource.data != null 
                  && this.dataSource.data.length > 0
                  ? Math.max.apply(Math, this.dataSource.data.map(function(o) { return o.id == null ? 0 : o.id; }))
                  : 0;

    this.eventReadService.getNewestEvents(lastId).subscribe(events => {
      this.dataSource.data.unshift.apply(this.dataSource.data, events);
      this.setTableData(this.dataSource.data);
    });
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
