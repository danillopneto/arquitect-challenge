import { environment } from './../../../environments/environment';
import { EventData } from './../../models/events/eventdata.model';
import { Configuration } from './configuration.model';
import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Observable, forkJoin } from "rxjs";
import { MessageService } from '../messages/message.service';
import { BaseService } from 'src/app/services/base.service';
import { EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ConfigurationService extends BaseService {
  processing: EventEmitter<any> = new EventEmitter();

  baseUrl = environment.apis.eventsData;

  constructor(private http: HttpClient, protected messageService: MessageService) {
    super(messageService);
  }

  startApplication(...configurations: Configuration[]): Observable<any> {
    var events = this.createEvents(configurations);
    var posts = events.map(e => this.http.post<EventData>(this.baseUrl, e));

    this.processing.emit(posts);

    this.messageService.showMessage(`${events.length} sensores foram enviados para processamento.`);
    return new Observable<any>();
  }

  stopApplication() {
    this.processing.emit();
  }

  createEventData(i: number, configuration: Configuration): EventData {
    var sensorNumber = (i + 1).toString();
    var sensorSufix = sensorNumber.length == 1 ? '0'.concat(sensorNumber) : sensorNumber;
    var sensorName = configuration.description.concat('.sensor', sensorSufix);

    var timestamp = new Date().getTime();
    var value = '';
    if ((i % 2) == 0) {
      value = this.getRandomIntInclusive(i, i * 100).toString();
    } else if (((i + 1)) % 5 == 0) {
      value = '';
    } else {
      value = `Event ${i + 1}`;
    };

    var event = new EventData(timestamp, sensorName, value);
    return event;
  }

  createEvents(configurations: Configuration[]): Array<EventData> {
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
}
