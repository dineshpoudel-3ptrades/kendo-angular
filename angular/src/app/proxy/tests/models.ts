import type { FullAuditedEntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface GetTestsInput extends PagedAndSortedResultRequestDto {
  filterText?: string;
  name?: string;
  ageMin?: number;
  ageMax?: number;
  dobMin?: string;
  dobMax?: string;
}

export interface TestCreateDto {
  name?: string;
  age: number;
  dob?: string;
}

export interface TestDto extends FullAuditedEntityDto<string> {
  name?: string;
  age: number;
  dob?: string;
  concurrencyStamp?: string;
}

export interface TestExcelDownloadDto {
  downloadToken?: string;
  filterText?: string;
  name?: string;
  ageMin?: number;
  ageMax?: number;
  dobMin?: string;
  dobMax?: string;
}

export interface TestUpdateDto {
  name?: string;
  age: number;
  dob?: string;
  concurrencyStamp?: string;
}
