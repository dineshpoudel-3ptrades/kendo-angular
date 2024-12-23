import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface FeedbackCreateDto {
  name?: string;
  number: number;
}

export interface FeedbackDto extends FullAuditedEntityDto<string> {
  name?: string;
  number: number;
  concurrencyStamp?: string;
}

export interface FeedbackExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
  numberMin?: number;
  numberMax?: number;
}

export interface FeedbackUpdateDto {
  name?: string;
  number: number;
  concurrencyStamp?: string;
}

export interface GetFeedbacksInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  numberMin?: number;
  numberMax?: number;
}
