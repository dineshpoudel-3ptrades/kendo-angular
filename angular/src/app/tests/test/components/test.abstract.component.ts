import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { TestDto } from '../../../proxy/tests/models';
import { TestViewService } from '../services/test.service';
import { TestDetailViewService } from '../services/test-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractTestComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(TestViewService);
  public readonly serviceDetail = inject(TestDetailViewService);
  protected title = '::Tests';
  public gridFilter;

  ngOnInit() {
    this.service.hookToQuery(this.gridFilter);
  }

  clearFilters() {
    this.service.clearFilters();
  }

  showForm() {
    this.serviceDetail.showForm();
  }

  create() {
    this.serviceDetail.selected = undefined;
    this.serviceDetail.showForm();
  }

  update(record: TestDto) {
    this.serviceDetail.update(record);
  }

  delete(record: TestDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
