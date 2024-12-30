import { Component, Input } from '@angular/core';
import { BaseFilterCellComponent, FilterService } from '@progress/kendo-angular-grid';
import { FilterDescriptor } from '@progress/kendo-data-query';
import { SVGIcon, xIcon } from '@progress/kendo-svg-icons';
import { KENDO_DATE } from '@progress/kendo-angular-intl';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { LabelModule } from '@progress/kendo-angular-label';
import { IntlModule } from '@progress/kendo-angular-intl';

interface CompositeFilterDescriptor {
  logic: 'or' | 'and';
  filters: Array<any>;
}

@Component({
  standalone: true,
  selector: 'date-range-filter-cell',
  imports: [KENDO_DATE, DateInputsModule],
  styles: [
    `
      kendo-daterange > kendo-dateinput.range-filter {
        display: inline-block;
      }
      .k-button {
        margin-left: 5px;
      }
    `,
  ],
  templateUrl: './date-range.component.html',
})
export class DateRangeComponent extends BaseFilterCellComponent {
  @Input() public filter: CompositeFilterDescriptor;

  @Input()
  public field: string;

  public xIcon: SVGIcon = xIcon;

  constructor(filterService: FilterService) {
    super(filterService);
  }

  public get start(): Date {
    const first = this.findByOperator('gte');

    return (first || <FilterDescriptor>{}).value;
  }

  public get end(): Date {
    const end = this.findByOperator('lte');
    return (end || <FilterDescriptor>{}).value;
  }

  public get hasFilter(): boolean {
    return this.filtersByField(this.field).length > 0;
  }

  public clearFilter(): void {
    this.filterService.filter(this.removeFilter(this.field));
  }

  public filterRange(start: Date, end: Date): void {
    this.filter = this.removeFilter(this.field);

    const filters = [];

    if (start) {
      filters.push({
        field: this.field,
        operator: 'gte',
        value: start,
      });
    }

    if (end) {
      filters.push({
        field: this.field,
        operator: 'lte',
        value: end,
      });
    }

    const root = this.filter || {
      logic: 'and',
      filters: [],
    };

    if (filters.length) {
      root.filters.push(...filters);
    }

    this.filterService.filter(root);
  }

  private findByOperator(op: string): FilterDescriptor {
    return this.filtersByField(this.field).filter(({ operator }) => operator === op)[0];
  }
}
