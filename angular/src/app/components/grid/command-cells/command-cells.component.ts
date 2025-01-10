import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-command-cells',
  standalone: true,
  imports: [],
  templateUrl: './command-cells.component.html',
  styleUrl: './command-cells.component.scss',
})
export class CommandCellsComponent {
  @Input() editButton: any;
  @Input() deleteButton: any;
  @Input() showDeleteButton: boolean;
  @Input() showEditButton: boolean;
  @Input() showPreviewButton: boolean;
  @Input() onActionHandler: Function;
  @Input() dataItem: any;
  @Input() rollbackButton: any;
  @Input() showRollbackButton: boolean;
  @Input() showFavoriteButton: boolean;
}
