import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PartService } from '@proxy/parts';
import { AbstractPartViewService } from 'src/app/parts/part/services/part.abstract.service';

@Component({
  selector: 'app-detail-page',
  standalone: true,
  imports: [],
  templateUrl: './detail-page.component.html',
  styleUrl: './detail-page.component.scss',
})
export class DetailPageComponent {
  public readonly service = inject(PartService);
  public id: string;
  public detailData: any;

  public getData() {
    this.detailData = this.service.get(this.id);
    console.log(this.detailData);
  }

  constructor(private route: ActivatedRoute) {
    this.route.queryParams.subscribe(params => {
      this.id = params['id'];
      this.getData();
    });
  }
}
