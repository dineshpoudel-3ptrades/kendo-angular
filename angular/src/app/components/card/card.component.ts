import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card',
  standalone: true,
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.scss'],
})
export class CardComponent {
  @Input() item!: any;
  @Input() title!: string;
  @Input() number!: number;

  @Input() removeHandler(dataItem: any) {}
  @Input() editHandler(record: any) {}
}
