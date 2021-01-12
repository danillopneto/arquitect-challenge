import { Component, OnInit } from '@angular/core';
import { Configuration } from './configuration.model';

@Component({
  selector: 'app-configuration',
  templateUrl: './configuration.component.html',
  styleUrls: ['./configuration.component.css']
})
export class ConfigurationComponent implements OnInit {
  north: Configuration = {
    description: 'brasil.norte',
    enabled: false,
    sensors: null
  };

  northeast: Configuration = {
    description: 'brasil.nordeste',
    enabled: false,
    sensors: null
  };

  south: Configuration = {
    description: 'brasil.sul',
    enabled: false,
    sensors: null
  };

  southeast: Configuration = {
    description: 'brasil.sudeste',
    enabled: false,
    sensors: null
  };

  constructor() {    
  }

  ngOnInit(): void {
  }

  turnOnOff(configuration: Configuration){
    if (!configuration.enabled) {
      configuration.sensors = null;
    }
  }

  onFormSubmit() {    
  }
}
