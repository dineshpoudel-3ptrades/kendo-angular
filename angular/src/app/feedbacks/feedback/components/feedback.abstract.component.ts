import { Directive, OnInit, inject } from '@angular/core';

import { ListService, TrackByService } from '@abp/ng.core';

import type { FeedbackDto } from '../../../proxy/feedbacks/models';
import { FeedbackViewService } from '../services/feedback.service';
import { FeedbackDetailViewService } from '../services/feedback-detail.service';

export const ChildTabDependencies = [];

export const ChildComponentDependencies = [];

@Directive({ standalone: true })
export abstract class AbstractFeedbackComponent implements OnInit {
  public readonly list = inject(ListService);
  public readonly track = inject(TrackByService);
  public readonly service = inject(FeedbackViewService);
  public readonly serviceDetail = inject(FeedbackDetailViewService);
  protected title = '::Feedbacks';

  ngOnInit() {
    this.service.hookToQuery();
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

  update(record: FeedbackDto) {
    this.serviceDetail.update(record);
  }

  delete(record: FeedbackDto) {
    this.service.delete(record);
  }

  exportToExcel() {
    this.service.exportToExcel();
  }
}
