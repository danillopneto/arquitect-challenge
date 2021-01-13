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
  treeControl = new NestedTreeControl<any>(node => node.children);
  dataSource = new MatTreeNestedDataSource<GroupEventData>();

  constructor(private eventsTagService: EventsTagService) {
    this.dataSource.data = new Array<any>();
  }

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents() {
    this.eventsTagService.getAll().subscribe(events => {
      this.dataSource.data = this.prepareDataToTree(events);
    });
  }

  prepareDataToTree(events: GroupEventData[]): any {
    let tree: Array<any> = [];

    /* It's not my best, but it does the work. */
    events.forEach(element => {
      var parts = element.tag.split('.');
      var isRoot = parts.length == 1;
      if (isRoot) {
        this.pushElement(tree, element);
      } else if (parts.length >= 2) {
        for (var i = 0; i < tree.length; i++) {
          if (tree[i].tag == parts[0]) {
            if (parts.length == 3) {
              for (var j = 0; j < tree[i].children.length; j++) {
                if (tree[i].children[j].tag == parts[0].concat('.', parts[1])) {
                  this.pushElement(tree[i].children[j].children, element);
                }
              }
            } else {
              this.pushElement(tree[i].children, element);
            }
          }
        }
      }
    });

    return tree;
  }

  pushElement(property: any[], element: any) {
    property.push({ tag: element.tag, count: element.count, children: [] });
  }

  hasChild = (_: number, node: GroupEventData) => !!node && node.children?.length > 0;
}
