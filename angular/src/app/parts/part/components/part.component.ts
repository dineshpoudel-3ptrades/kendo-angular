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

import { PartViewService } from '../services/part.service';
import { PartDetailViewService } from '../services/part-detail.service';
import { PartDetailModalComponent } from './part-detail.component';
import {
  AbstractPartComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './part.abstract.component';
import { GridComponent } from 'src/app/components/grid/grid.component';
import { CardViewComponent } from '../../../components/card-view/card-view.component';

@Component({
  selector: 'app-part',
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
    PartDetailModalComponent,
    GridComponent,
    ...ChildComponentDependencies,
    CardViewComponent,
  ],
  providers: [
    ListService,
    PartViewService,
    PartDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './part.component.html',
  styleUrls: ['./part.component.scss'],
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class PartComponent extends AbstractPartComponent {
  public gridColumns = [
    { field: 'name', header: 'Name' },
    { field: 'number', header: 'Number' },
  ];

  public selectedView: 'List' | 'Grid' = 'List';

  public viewSelector(view: 'List' | 'Grid') {
    this.selectedView = view; // Change the view
  }
}
