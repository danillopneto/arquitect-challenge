import { Component, OnInit } from '@angular/core';
import { Configuration } from './configuration.model';
import { ConfigurationService } from './configuration.service';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit {
  formEnabled: boolean = true;
  configurations!: Configuration[];

  constructor(private configurationService: ConfigurationService) {
    this.formEnabled = !configurationService.running;
  }

  ngOnInit(): void {
    this.configurations = this.configurationService.getConfigurations();
  }

  turnOnOff(configuration: Configuration) {
    if (!configuration.enabled) {
      configuration.sensors = null;
    }
  }

  turnOn() {
    this.configurationService.startApplication(this.configurations);
    this.formEnabled = false;
  }

  turnOff() {
    this.configurationService.stopApplication();
    this.formEnabled = true;
  }
}
