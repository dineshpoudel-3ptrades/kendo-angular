import { Component, Input, SimpleChanges } from '@angular/core';
import { CardComponent } from '../card/card.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-card-view',
  standalone: true,
  imports: [CardComponent, CommonModule],
  templateUrl: './card-view.component.html',
  styleUrls: ['./card-view.component.scss'],
})
export class CardViewComponent {
  @Input() items;
  @Input() onEdit(item: any) {
    return item;
  }
  @Input() onDelete(item: any) {
    return item;
  }
}
