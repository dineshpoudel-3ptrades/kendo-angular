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
    CardViewComponent,
    ...ChildComponentDependencies,
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
    { field: 'number', header: 'Number', filter: 'numeric' },
  ];

  public dataStateChange(state: any) {
    this.gridFilter = state;
    this.count = state.maxResultCount;
    this.skip = state.skipCount;
    this.service.hookToQuery(this.gridFilter, this.count, this.skip);
  }

  public gridSelect(state: any) {
    console.log(state);
  }

  public selectedView: 'List' | 'Grid' = 'Grid';

  public viewSelector(view: 'List' | 'Grid') {
    this.selectedView = view;
  }
}
