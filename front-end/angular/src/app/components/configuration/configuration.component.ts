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
    console.log(this.configurations);
  }

  turnOnOff(configuration: Configuration) {
    if (!configuration.enabled) {
      configuration.sensors = null;
    }
  }

  turnOn() {
    console.log("Processing...");
    this.configurationService.startApplication(this.configurations);

    this.formEnabled = false;
  }

  turnOff() {
    console.log("Stopping...");
    this.configurationService.stopApplication();

    this.formEnabled = true;
  }
}
