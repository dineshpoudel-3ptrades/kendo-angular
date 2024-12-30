import { Component, Input, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { PDFModule } from '@progress/kendo-angular-grid';
import { SVGIcon, filePdfIcon } from '@progress/kendo-svg-icons';
import { DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgFor } from '@angular/common';
import { CommercialUiModule, AdvancedEntityFiltersModule } from '@volo/abp.commercial.ng.ui';
import { Router } from '@angular/router';
import { State, process } from '@progress/kendo-data-query';
import { DateRangeComponent } from '../filters/date-range/date-range.component';
import { DateRangeModule, DateInputModule } from '@progress/kendo-angular-dateinputs';
import { BreadcrumbComponent } from '../lepton-components/breadcrumb/breadcrumb.component';

@Component({
  standalone: true,
  imports: [
    GridModule,
    DateRangeModule,
    DateInputModule,
    PDFModule,
    NgFor,
    AdvancedEntityFiltersModule,
    CommercialUiModule,
    DateRangeComponent,
    BreadcrumbComponent,
  ],
  selector: 'default-grid',
  templateUrl: './grid.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./page-template.css'],
})
export class GridComponent {
  @Input() gridData: any[] = [];
  @Input() gridColumns: any[] = [];
  @Input() height = 510;
  @Input() sortable = true;
  @Input() filterable = true;
  @Input() pageable = true;
  @Input() groupable = false;
  @Input() list: any;
  @Input() service: any;
  @Input() removeHandler(dataItem: any) {}
  @Input() editHandler!: (record: any) => void;

  @Input() dataStateChange(dataState: any) {}
  @Input() onGridSelect(dataItem: any) {}

  public data = {
    data: this.gridData,
    total: 100,
  };

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['gridData']) {
      this.data = {
        data: this.gridData,
        total: 100,
      };
    }
  }

  public state: State = {
    skip: 0,
    take: 5,
    filter: {
      logic: 'and',
      filters: [],
    },
    group: [],
    sort: [],
  };

  constructor(private router: Router) {}

  navigateTo(id: string) {
    this.router.navigate(['/part'], { queryParams: { id } });
  }

  public isDateString(value) {
    const parsedDate = new Date(value);
    return !isNaN(parsedDate.getTime());
  }

  public onSelectionChange = (event: any) => {
    console.log(event);
    const id = event.selectedRows[0].dataItem.id;
    this.navigateTo(id);
    this.onGridSelect(event.selectedRows[0].dataItem.id);
  };

  public onDataStateChange = (event: DataStateChangeEvent) => {
    this.state = event;
    let filterObject = {};
    event.filter?.filters?.forEach((f: any) => {
      if (typeof f.value === 'object') {
        const date = new Date(f.value);
        const formattedDate = date.toISOString().split('T')[0];
        f.value = formattedDate;
      }
      console.log(f);
      switch (f.operator) {
        case 'contains':
          return (filterObject[f.field] = f.value);
        case 'gte':
        case 'gt':
          return (filterObject[`${f.field}Min`] = f.value);
        case 'lte':
        case 'lt':
          return (filterObject[`${f.field}Max`] = f.value);
        case 'eq':
          return (filterObject[`${f.field}`] = f.value);
        default:
          return;
      }
    });

    const convertedState = {
      maxResultCount: this.state.take,
      skipCount: this.state.skip,
      ...filterObject,
    };
    console.log(convertedState);
    this.dataStateChange(convertedState);
  };

  @Input() onAdd: () => void = () => {};

  public filePdfIcon: SVGIcon = filePdfIcon;
}
