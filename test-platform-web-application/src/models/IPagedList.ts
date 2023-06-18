export interface IPagedList<T> {
    pageSize: number;
    pageNumber: number;
    totalCount: number;
    totalPages: number;
    items: T[];
}

