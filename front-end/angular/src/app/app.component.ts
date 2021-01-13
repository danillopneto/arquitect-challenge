import { ConfigurationService } from './components/configuration/configuration.service';
import { Component, EventEmitter } from '@angular/core';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: 'app.component.html'
})
export class AppComponent {
  nome = 'Danillo';
  eventsRunning: any;

  constructor(private configurationService: ConfigurationService) {
    this.configurationService.processing.subscribe(
      (posts: any) => {
        /* Stopping current sensors runnings */
        clearInterval(this.eventsRunning);

        if (posts != null) {
          this.eventsRunning = setInterval(function () {
            forkJoin(posts).subscribe(result => {
              console.log(result);
            });
          }, 1000);
        }
      });
  }
}
