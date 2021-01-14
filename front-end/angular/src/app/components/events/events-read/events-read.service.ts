import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError, map } from 'rxjs/operators';
import { BaseService } from 'src/app/services/base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MessageService } from '../../messages/message.service';
import { EventData } from 'src/app/models/events/eventdata.model';

@Injectable({
  providedIn: 'root'
})
export class EventReadService extends BaseService {
  baseUrl = environment.apis.eventsData;

  constructor(private http: HttpClient, protected messageService: MessageService) {
    super(messageService);
  }

  getAll(): Observable<EventData[]> {
    return this.http.get<EventData[]>(this.baseUrl).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }

  getNewestEvents(lastEventId: number): Observable<EventData[]> {
    return this.http.get<EventData[]>(`${this.baseUrl}/GetNewestEvents?lastEventId=${lastEventId}`).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }
}
