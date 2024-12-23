import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetPartsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  numberMin?: number;
  numberMax?: number;
}

export interface PartCreateDto {
  name?: string;
  number: number;
}

export interface PartDto extends FullAuditedEntityDto<string> {
  name?: string;
  number: number;
  concurrencyStamp?: string;
}

export interface PartExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
  numberMin?: number;
  numberMax?: number;
}

export interface PartUpdateDto {
  name?: string;
  number: number;
  concurrencyStamp?: string;
}
