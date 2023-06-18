export interface IPaginatedQuery {
    pageSize?: number;
    page?: number;
}

export interface IPaginatedQueryWithSearch extends IPaginatedQuery {
    searchedText?: string;
}