import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/services/base.service';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageService } from '../../messages/message.service';
import { catchError, map } from 'rxjs/operators';
import { GroupEventByHour } from 'src/app/models/events/groupeventbyhour.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventsByhourService extends BaseService {
  baseUrl: string = environment.apis.eventsData;

  constructor(private http: HttpClient, protected messageService: MessageService) {
    super(messageService);
  }

  getEventsGroupedByHour(date: Date): Observable<GroupEventByHour[]> {
    var dateString = new Date(date.getTime() - (date.getTimezoneOffset() * 60000)).toISOString().split("T")[0];
    return this.http.get<GroupEventByHour[]>(`${this.baseUrl}/GetEventsGroupedByHour?date=${dateString}`).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }
}
