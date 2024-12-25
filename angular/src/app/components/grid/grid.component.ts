import { Component, Input, ViewEncapsulation } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { PDFModule } from '@progress/kendo-angular-grid';
import { SVGIcon, filePdfIcon } from '@progress/kendo-svg-icons';
import { NgFor } from '@angular/common';

@Component({
  standalone: true,
  imports: [GridModule, PDFModule, NgFor],
  selector: 'default-grid',
  templateUrl: './grid.component.html',
  encapsulation: ViewEncapsulation.None,
  styleUrls: ['./page-template.css'],
})
export class GridComponent {
  @Input() gridData: any[] = [];
  @Input() gridColumns: any[] = [];
  @Input() pageSize = 8;
  @Input() height = 510;
  @Input() sortable = true;
  @Input() filterable = true;
  @Input() pageable = true;
  @Input() groupable = false;

  @Input() removeHandler(dataItem: any) {}
  @Input() editHandler!: (record: any) => void;

  @Input() onAdd: () => void = () => {};

  public filePdfIcon: SVGIcon = filePdfIcon;
}
