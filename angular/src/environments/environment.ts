 import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44332/',
  redirectUri: baseUrl,
  clientId: 'abp_App',
  responseType: 'code',
  scope: 'offline_access abp',
  requireHttps: true,
  impersonation: {
    tenantImpersonation: true,
    userImpersonation: true,
  }
};

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'abp',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44332',
      rootNamespace: 'abp',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
} as Environment;
