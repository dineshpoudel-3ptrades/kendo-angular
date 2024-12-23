import type {
  GetPartsInput,
  PartCreateDto,
  PartDto,
  PartExcelDownloadDto,
  PartUpdateDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppFileDescriptorDto, DownloadTokenResultDto, GetFileInput } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class PartService {
  apiName = 'Default';

  create = (input: PartCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PartDto>(
      {
        method: 'POST',
        url: '/api/app/parts',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/parts/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetPartsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/parts/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          numberMin: input.numberMin,
          numberMax: input.numberMax,
        },
      },
      { apiName: this.apiName, ...config },
    );

  deleteByIds = (partIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/parts',
        params: { partIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PartDto>(
      {
        method: 'GET',
        url: `/api/app/parts/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/parts/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/parts/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetPartsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<PartDto>>(
      {
        method: 'GET',
        url: '/api/app/parts',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          numberMin: input.numberMin,
          numberMax: input.numberMax,
        },
      },
      { apiName: this.apiName, ...config },
    );

  getListAsExcelFile = (input: PartExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/parts/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
          numberMin: input.numberMin,
          numberMax: input.numberMax,
        },
      },
      { apiName: this.apiName, ...config },
    );

  update = (id: string, input: PartUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PartDto>(
      {
        method: 'PUT',
        url: `/api/app/parts/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/parts/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
