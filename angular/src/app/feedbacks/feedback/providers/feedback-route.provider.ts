import { APP_INITIALIZER, inject } from '@angular/core';
import { ABP, RoutesService } from '@abp/ng.core';
import { FEEDBACK_BASE_ROUTES } from './feedback-base.routes';

export const FEEDBACKS_FEEDBACK_ROUTE_PROVIDER = [
  {
    provide: APP_INITIALIZER,
    multi: true,
    useFactory: configureRoutes,
  },
];

function configureRoutes() {
  const routesService = inject(RoutesService);

  return () => {
    const routes: ABP.Route[] = [...FEEDBACK_BASE_ROUTES];
    routesService.add(routes);
  };
}
