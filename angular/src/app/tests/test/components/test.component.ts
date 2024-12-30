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

import { TestViewService } from '../services/test.service';
import { TestDetailViewService } from '../services/test-detail.service';
import { TestDetailModalComponent } from './test-detail.component';
import {
  AbstractTestComponent,
  ChildTabDependencies,
  ChildComponentDependencies,
} from './test.abstract.component';
import { GridComponent } from 'src/app/components/grid/grid.component';

import { CardViewComponent } from 'src/app/components/card-view/card-view.component';
import { filter } from 'rxjs';

@Component({
  selector: 'app-test',
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
    TestDetailModalComponent,
    GridComponent,
    CardViewComponent,
    ...ChildComponentDependencies,
  ],
  providers: [
    ListService,
    TestViewService,
    TestDetailViewService,
    { provide: NgbDateAdapter, useClass: DateAdapter },
    { provide: NgbTimeAdapter, useClass: TimeAdapter },
  ],
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.scss'],
  styles: `
    ::ng-deep.datatable-row-detail {
      background: transparent !important;
    }
  `,
})
export class TestComponent extends AbstractTestComponent {
  public gridColumns = [
    { field: 'name', header: 'Name' },
    { field: 'age', header: 'Age', filter: 'numeric' },
    { field: 'dob', header: 'DOB', filter: 'date', format: 'MM/dd/yyyy' },
  ];

  public dataStateChange(state: any) {
    this.gridFilter = state;
    this.service.hookToQuery(this.gridFilter);
  }

  public selectedView: 'List' | 'Grid' = 'Grid';

  public viewSelector(view: 'List' | 'Grid') {
    this.selectedView = view;
  }
}
