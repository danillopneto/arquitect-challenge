import { GroupEventData } from './../../../models/events/groupeventdata.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';
import { environment } from 'src/environments/environment';
import { MessageService } from '../../messages/message.service';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class EventsTagService extends BaseService {
  baseUrl = environment.apis.eventsData;

  constructor(private http: HttpClient, protected messageService: MessageService) {
    super(messageService);
  }

  getAll(): Observable<GroupEventData[]> {
    return this.http.get<GroupEventData[]>(`${this.baseUrl}/GetAllGroupedByTag`).pipe(
      map((obj) => obj),
      catchError((e) => this.errorHandler(e))
    );
  }
}
