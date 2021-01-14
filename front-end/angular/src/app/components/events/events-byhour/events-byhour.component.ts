import { EventsByhourService } from './events-byhour.service';
import { AfterViewInit, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label } from 'ng2-charts';

@Component({
  selector: 'app-events-byhour',
  templateUrl: './events-byhour.component.html',
  styleUrls: ['./events-byhour.component.css']
})
export class EventsByhourComponent implements OnInit, OnDestroy {
  public lineChartData: ChartDataSets[] = [];
  public lineChartLabels: Label[] = [...Array(24).keys()].map((e) => e.toString().padStart(2, '0').concat(':00'));
  public lineChartOptions: ChartOptions = { responsive: false };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(255,0,0,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartType: ChartType = 'line';
  public lineChartPlugins = [];

  autoUpdate = false;
  updateRunning: any;

  constructor(private eventsByhourService: EventsByhourService) { }

  ngOnInit() {
    this.loadEvents();
  }

  ngOnDestroy() {
    clearInterval(this.updateRunning);
  }

  updateSeconds() {
    if (this.autoUpdate) {
      var _this = this;
      this.updateRunning = setInterval(function () {
        _this.loadEvents();
      }, 5000);
    } else {
      clearInterval(this.updateRunning);
    }
  }

  loadEvents() {
    var today = new Date();
    this.eventsByhourService.getEventsGroupedByHour(today).subscribe((group) => {
      this.lineChartData = group.map((g) => ({ data: g.eventsByHour, label: g.tag }));
    })
  }
}
