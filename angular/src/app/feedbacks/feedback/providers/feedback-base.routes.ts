import { ABP, eLayoutType } from '@abp/ng.core';

export const FEEDBACK_BASE_ROUTES: ABP.Route[] = [
  {
    path: '/feedbacks',
    iconClass: 'fas fa-file-alt',
    name: '::Menu:Feedbacks',
    layout: eLayoutType.application,
    requiredPolicy: 'abp.Feedbacks',
    breadcrumbText: '::Feedbacks',
  },
];
