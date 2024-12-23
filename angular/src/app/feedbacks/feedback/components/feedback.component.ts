import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
  NgbDateAdapter,
  NgbTimeAdapter,
  NgbCollapseModule,
  NgbDatepickerModule,
  NgbTimepickerModule,
  NgbDropdownModule,
} from '@ng-bootstrap/ng-bootstrap';
import { NgxValidateCoreModule } from '@ngx-validate/core';
import { ListService, CoreModule } from '@abp/ng.core';
import { ThemeSharedModule, DateAdapter, TimeAdapter } from '@abp/ng.theme.shared';
import { PageModule } from '@abp/ng.components/page';
import { CommercialUiModule } from '@volo/abp.commercial.ng.ui';

import { FeedbackViewService } from '../services/feedback.service';
import { FeedbackDetailViewService } from '../services/feedback-detail.service';
import { FeedbackDetailModalComponent } from './feedback-detail.component';
import {
  AbstractFeedbackComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './feedback.abstract.component';

@Component({
  selector: 'app-feedback',
  changeDetection: ChangeDetectionStrategy.Default,
  standalone: true,
  imports: [
    ...ChildTabDependencies,
    NgbCollapseModule,
    NgbDatepickerModule,
    NgbTimepickerModule,
    NgbDropdownModule,

    NgxValidateCoreModule,

    PageModule,
    CoreModule,
    ThemeSharedModule,
    CommercialUiModule,
    FeedbackDetailModalComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    FeedbackViewService,
    FeedbackDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './feedback.component.html',
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class FeedbackComponent extends AbstractFeedbackComponent {}
