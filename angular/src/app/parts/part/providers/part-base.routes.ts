import { ABP, eLayoutType } from '@abp/ng.core';

export const PART_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/parts',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:Parts',
    layout: eLayoutType.application,
    requiredPolicy: 'abp.Parts',
    breadcrumbText: '::Parts',
  },
];
