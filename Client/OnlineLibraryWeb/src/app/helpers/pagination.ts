export interface Pagination {
  TotalCount: number;
  PageSize: number;
  CurrentPage: number;
  TotalPages: number;
  HasNext: boolean;
  HasPrevious: boolean;
}
