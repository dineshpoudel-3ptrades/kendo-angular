import type {
  FeedbackCreateDto,
  FeedbackDto,
  FeedbackExcelDownloadDto,
  FeedbackUpdateDto,
  GetFeedbacksInput,
} from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { AppFileDescriptorDto, DownloadTokenResultDto, GetFileInput } from '../shared/models';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  apiName = 'Default';

  create = (input: FeedbackCreateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FeedbackDto>(
      {
        method: 'POST',
        url: '/api/app/feedbacks',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: `/api/app/feedbacks/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  deleteAll = (input: GetFeedbacksInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/feedbacks/all',
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

  deleteByIds = (feedbackIds: string[], config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>(
      {
        method: 'DELETE',
        url: '/api/app/feedbacks',
        params: { feedbackIds },
      },
      { apiName: this.apiName, ...config },
    );

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FeedbackDto>(
      {
        method: 'GET',
        url: `/api/app/feedbacks/${id}`,
      },
      { apiName: this.apiName, ...config },
    );

  getDownloadToken = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, DownloadTokenResultDto>(
      {
        method: 'GET',
        url: '/api/app/feedbacks/download-token',
      },
      { apiName: this.apiName, ...config },
    );

  getFile = (input: GetFileInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/feedbacks/file',
        params: { downloadToken: input.downloadToken, fileId: input.fileId },
      },
      { apiName: this.apiName, ...config },
    );

  getList = (input: GetFeedbacksInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<FeedbackDto>>(
      {
        method: 'GET',
        url: '/api/app/feedbacks',
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

  getListAsExcelFile = (input: FeedbackExcelDownloadDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, Blob>(
      {
        method: 'GET',
        responseType: 'blob',
        url: '/api/app/feedbacks/as-excel-file',
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

  update = (id: string, input: FeedbackUpdateDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, FeedbackDto>(
      {
        method: 'PUT',
        url: `/api/app/feedbacks/${id}`,
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  uploadFile = (input: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, AppFileDescriptorDto>(
      {
        method: 'POST',
        url: '/api/app/feedbacks/upload-file',
        body: input,
      },
      { apiName: this.apiName, ...config },
    );

  constructor(private restService: RestService) {}
}
