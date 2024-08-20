import {Pagination} from "./pagination";

export interface PagedList<T> {
  items: T[];
  pagination: Pagination;
}
