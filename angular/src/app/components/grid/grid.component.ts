import { Component, Input, SimpleChanges, ViewEncapsulation } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { PDFModule, ExcelModule } from '@progress/kendo-angular-grid';
import { SVGIcon, filePdfIcon, fileExcelIcon } from '@progress/kendo-svg-icons';
import { DataStateChangeEvent } from '@progress/kendo-angular-grid';
import { NgFor, NgIf } from '@angular/common';
import { CommercialUiModule, AdvancedEntityFiltersModule } from '@volo/abp.commercial.ng.ui';
import { Router } from '@angular/router';
import { State, process } from '@progress/kendo-data-query';
import { DateRangeComponent } from '../filters/date-range/date-range.component';
import { DateRangeModule, DateInputModule } from '@progress/kendo-angular-dateinputs';
import { BreadcrumbComponent } from '../lepton-components/breadcrumb/breadcrumb.component';
import { IconsModule } from '@progress/kendo-angular-icons';
import { LabelModule } from '@progress/kendo-angular-label';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { NgbDropdownModule } from '@ng-bootstrap/ng-bootstrap';
import { SelectAllCheckboxState } from '@progress/kendo-angular-grid';
@Component({
  standalone: true,
  imports: [
    GridModule,
    NgbDropdownModule,
    DateRangeModule,
    DateInputModule,
    PDFModule,
    ExcelModule,
    LabelModule,
    InputsModule,
    IconsModule,
    NgFor,
    NgIf,
    AdvancedEntityFiltersModule,
    CommercialUiModule,
    DateRangeComponent,
  ],
  selector: 'default-grid',
  templateUrl: './grid.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./grid.component.css'],
})
export class GridComponent {
  @Input() gridData: any[] = [];
  @Input() items = [];
  @Input() list;
  @Input() service;
  @Input() gridColumns = [];
  @Input() height = 510;
  @Input() totalCount = 10;
  @Input() filterCells;
  @Input() columnMenuCells;
  @Input() filterType = [];
  @Input() commandCellWidth = 100;
  @Input() selectedField = 'delete';
  @Input() dataItemKey = 'id';
  @Input() state: State = {
    skip: 0,
    take: 5,
    filter: {
      logic: 'and',
      filters: [],
    },
    group: [],
    sort: [],
  };

  @Input() sortable = true;
  @Input() filterable = false;
  @Input() pageable = true;
  @Input() groupable = false;
  @Input() showCreateNew = true;
  @Input() showEditButton = true;
  @Input() showPreviewButton = false;
  @Input() showFavoriteButton = true;
  @Input() showDeleteButton = true;
  @Input() showDeleteAllButton = true;
  @Input() showExportCSVButton = false;
  @Input() showExportExcelButton = true;
  @Input() showExportPdfButton = false;
  @Input() showExportJsonButton = false;
  @Input() showImportPartsButton = false;
  @Input() showGridToolbar = true;
  @Input() showAddButton = true;
  @Input() showSelectExisting = false;
  @Input() showCommandCell = true;
  @Input() showFilterableButton = true;
  @Input() showSaveButton = false;
  @Input() showCancelButton = false;
  @Input() showCreateNewButton = false;
  @Input() showRollbackButton = false;
  @Input() removeHandler(dataItem: any) {}
  @Input() editHandler!: (record: any) => void;
  @Input() dataStateChange(dataState: any) {}
  @Input() onGridSelect(dataItem: any) {}
  @Input() onAdd: () => void = () => {};
  @Input() onHandleDelteAll: (event: any) => void;

  public pageSizes = [2, 5, 10, 20];
  public toggleDelete: boolean = false;
  public filePdfIcon: SVGIcon = filePdfIcon;
  public fileExcelIcon: SVGIcon = fileExcelIcon;
  public mySelection = [];
  public selectAllState: SelectAllCheckboxState = 'unchecked';

  public onSelectedKeysChange(): void {
    const len = this.mySelection.length;
    if (len === 0) {
      this.selectAllState = 'unchecked';
    } else if (len > 0 && len < this.gridData.length) {
      this.selectAllState = 'indeterminate';
      this.service.onSelect({ selected: this.getSelectedItems() });
    } else {
      this.selectAllState = 'checked';
      this.service.selectAll();
    }
  }

  public getSelectedItems() {
    const matchingObjects = this.gridData.filter(obj => this.mySelection.includes(obj.id));
    return matchingObjects;
  }

  public handleDeleteAll() {
    this.service.bulkDelete();
  }

  public onSelectAllChange(checkedState: SelectAllCheckboxState): void {
    if (checkedState === 'checked') {
      this.mySelection = this.gridData.map(item => {
        return item.id;
      });
      this.service.selectAll();
      this.selectAllState = 'checked';
    } else {
      this.service.onSelect({ selected: [] });
      this.mySelection = [];
      this.selectAllState = 'unchecked';
    }
  }

  constructor(private router: Router) {}

  public data = {
    data: this.gridData,
    total: this.totalCount,
  };

  toggleFilter() {
    this.filterable = !this.filterable;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['gridData']) {
      this.data = {
        data: this.gridData,
        total: this.totalCount,
      };
    }
  }

  navigateTo(id: string) {
    this.router.navigate(['/part'], { queryParams: { id } });
  }

  public isDateString(value) {
    const parsedDate = new Date(value);
    return !isNaN(parsedDate.getTime());
  }

  public onSelectionChange = (event: any) => {
    // const id = event.selectedRows[0].dataItem.id;
    // this.onGridSelect(event.selectedRows[0].dataItem.id);
  };

  public onHeaderSelectionChange = (event: any) => {
    console.log(event);
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
    this.dataStateChange(convertedState);
  };
}
