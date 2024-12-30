import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { PartDto } from '../../../proxy/parts/models';
import { PartViewService } from '../services/part.service';
import { PartDetailViewService } from '../services/part-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractPartComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(PartViewService);
  public readonly serviceDetail = inject(PartDetailViewService);
  protected title = '::Parts';
  public gridFilter;
  public skip = 0;
  public count = 5;

  ngOnInit() {
    this.service.hookToQuery(this.gridFilter, this.count, this.skip);
    this.service.filter();
  }

  clearFilters() {
    this.service.clearFilters();
  }
  filter() {
    this.service.filter();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: PartDto) {
    this.serviceDetail.update(record);
  }

  delete(record: PartDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
