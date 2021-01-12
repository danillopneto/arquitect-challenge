import { EMPTY, Observable } from 'rxjs';
import { MessageService } from '../components/messages/message.service';

export abstract class BaseService {
    abstract baseUrl: string;

    constructor(
        protected messageService: MessageService) { }

    errorHandler(e: any): Observable<any> {
        console.log(e);
        this.messageService.showMessage("Ocorreu um erro!", true);
        return EMPTY;
    }
}
