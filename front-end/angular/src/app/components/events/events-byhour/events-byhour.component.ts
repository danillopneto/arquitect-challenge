import { map } from 'rxjs/operators';
import { GroupEventByHour } from './../../../models/events/groupeventbyhour.model';
import { EventsByhourService } from './events-byhour.service';
import { Component, OnDestroy, OnInit } from '@angular/core';
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
  public lineChartOptions: ChartOptions = { responsive: true };
  public lineChartColors: Color[] = [
    {
      borderColor: 'black',
      backgroundColor: 'rgba(255,0,0,0.3)',
    },
  ];
  public lineChartLegend = true;
  public lineChartPlugins = [];

  groupData: GroupEventByHour[] = new Array<GroupEventByHour>();
  autoUpdate = false;
  updateRunning: any;
  chartType: string = "1";

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
      this.groupData = group;
      this.changeChartType();
    })
  }

  changeChartType() {
    var events = this.chartType == "1"
                  ? this.groupData.filter((g) => g.isRegion)
                  : this.groupData.filter((g) => !g.isRegion);

      /* Just like in the tag's component: It's not my best, but it does the work. */
      var result: any = {};
      events.forEach(element => {
        if (result[element.tag] == null) {
          result[element.tag] = { 
            data: [...Array(24).keys()].map((e) => 0)
          };
        }

        var hour = new Date(element.timestamp * 1000).getHours();
        result[element.tag].data[hour] = element.count;
      });

      this.lineChartData = [];
      for (var name in result) {
        this.lineChartData.push({ label: name, data: result[name].data });
      }
  }
}
