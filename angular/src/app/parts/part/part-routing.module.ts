import { authGuard, permissionGuard } from '@abp/ng.core';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PartComponent } from './components/part.component';
import { DetailPageComponent } from 'src/app/components/detail-page/detail-page.component';

export const routes: Routes = [
  {
    path: '',
    component: PartComponent,
    canActivate: [authGuard, permissionGuard],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PartRoutingModule {}
