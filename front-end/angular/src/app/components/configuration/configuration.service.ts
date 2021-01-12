import { EventData } from './../../models/events/eventdata.model';
import { Configuration } from './configuration.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, EMPTY } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { MessageService } from '../messages/message.service';
import { ɵELEMENT_PROBE_PROVIDERS } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService {
  baseUrl = "http://localhost:8080/api/v1/Event";

  constructor(private http: HttpClient, protected messageService: MessageService) { }

  startApplication(...configurations: Configuration[]): Observable<any> {
    var events = this.createEvents(configurations);
    console.log(events);
    return new Observable<any>();

    /*return this.http.post<EventData>(this.baseUrl, configuration).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );/*/
  }

  createEventData(i: number, configuration: Configuration): EventData {
    var sensorNumber = (i + 1).toString();
    var sensorSufix = i.toString().length == 1 ? '0'.concat(sensorNumber) : sensorNumber;
    var sensorName = configuration.description.concat('.sensor', sensorSufix);

    var timestamp = new Date().getTime();
    var value = '';
    if ((i % 2) == 0){
      value = this.getRandomIntInclusive(i, i * 100).toString();
    } else if (((i + 1)) % 5 == 0) {
      value = '';
    } else {
      value = `Event ${i + 1}`;
    };

    var event = new EventData(timestamp, sensorName, value);
    return event;
  }

  createEvents(configurations: Configuration[]) : Array<EventData> {
    var events = Array<EventData>();
    /* Creating events to be dispatch. */
    configurations.forEach(element => {
      if (element.enabled
        && element.sensors != null) {
        for (var i = 0; i < element.sensors; i++) {
            var eventData = this.createEventData(i, element);
            events.push(eventData);
        }
      }
    });

    return events;
  }

  getRandomIntInclusive(min: number, max: number) {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  }

  errorHandler(e: any): Observable<any> {
    console.log(e);
    this.messageService.showMessage("Ocorreu um erro!", true);
    return EMPTY;
  }
}
