import { ABP, eLayoutType } from '@abp/ng.core';

export const TEST_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/tests',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:Tests',
    layout: eLayoutType.application,
    requiredPolicy: 'abp.Tests',
    breadcrumbText: '::Tests',
  },
];
