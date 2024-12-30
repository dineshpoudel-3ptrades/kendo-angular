import type {
  GetTestsInput,
  TestCreateDto,
  TestDto,
  TestExcelDownloadDto,
  TestUpdateDto,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppFileDescriptorDto, DownloadTokenResultDto, GetFileInput } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class TestService {
  apiName = 'Default';

  create = (input: TestCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TestDto>(
      {
        method: 'POST',
        url: '/api/app/tests',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/tests/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  deleteAll = (input: GetTestsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/tests/all',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          ageMin: input.ageMin,
          ageMax: input.ageMax,
          dobMin: input.dobMin,
          dobMax: input.dobMax,
        },
      },
      { apiName: this.apiName, ...config }
    );

  deleteByIds = (testIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/tests',
        params: { testIds },
      },
      { apiName: this.apiName, ...config }
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TestDto>(
      {
        method: 'GET',
        url: `/api/app/tests/${id}`,
      },
      { apiName: this.apiName, ...config }
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/tests/download-token',
      },
      { apiName: this.apiName, ...config }
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/tests/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config }
    );

  getList = (input: GetTestsInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<TestDto>>(
      {
        method: 'GET',
        url: '/api/app/tests',
        params: {
          filterText: input.filterText,
          sorting: input.sorting,
          skipCount: input.skipCount,
          maxResultCount: input.maxResultCount,
          name: input.name,
          ageMin: input.ageMin,
          ageMax: input.ageMax,
          dobMin: input.dobMin,
          dobMax: input.dobMax,
        },
      },
      { apiName: this.apiName, ...config }
    );

  getListAsExcelFile = (input: TestExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/tests/as-excel-file',
        params: {
          downloadToken: input.downloadToken,
          filterText: input.filterText,
          name: input.name,
          ageMin: input.ageMin,
          ageMax: input.ageMax,
          dobMin: input.dobMin,
          dobMax: input.dobMax,
        },
      },
      { apiName: this.apiName, ...config }
    );

  update = (id: string, input: TestUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, TestDto>(
      {
        method: 'PUT',
        url: `/api/app/tests/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/tests/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config }
    );

  constructor(private restService: RestService) {}
}
