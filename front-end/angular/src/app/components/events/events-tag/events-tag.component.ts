import { GroupEventData } from './../../../models/events/groupeventdata.model';
import { NestedTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { EventsTagService } from './events-tag.service';

@Component({
  selector: 'app-events-tag',
  templateUrl: './events-tag.component.html',
  styleUrls: ['./events-tag.component.css']
})

export class EventsTagComponent implements OnInit {
  treeControl = new NestedTreeControl<GroupEventData>(node => node.children);
  dataSource = new MatTreeNestedDataSource<GroupEventData>();

  constructor(private eventsTagService: EventsTagService) {
    this.dataSource.data = new Array<GroupEventData>();
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents() {
    this.eventsTagService.getAll().subscribe(events => {
      this.dataSource.data = events;
    });
  }

  hasChild = (_: number, node: GroupEventData) => !!node && node.children?.length > 0;
}
