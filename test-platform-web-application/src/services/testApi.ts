import {ITest} from "../models/ITest";
import {HttpMethod} from "../common/HttpMethod";
import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {IPagedList} from "../models/IPagedList";
import {IPaginatedQueryWithSearch} from "../models/IPaginatedQuery";

import {ITestResult} from "../models/ITestResult";
import {setTestResult} from "../redux/slices/testResultState";

export const testApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getTests: builder.query<IPagedList<ITest>, IPaginatedQueryWithSearch | void>({
            query: query => ({
                url: `${ApiTags.Test}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Test, id} as const)),
                {type: ApiTags.Test, id: "LIST"},
            ],
        }),
        getTest: builder.query<ITest, number>({
            query: id => ({
                url: `${ApiTags.Test}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Test, id},
            ]
        }),
        checkTest: builder.mutation<ITestResult, ITest>({
            query: test => ({
                url: `${ApiTags.Test}/check`,
                method: HttpMethod.POST,
                body: {
                    testId: test.id,
                    questions: test.questions
                },
            }),
            async onQueryStarted(arg, {dispatch, queryFulfilled}) {
                try {
                    const { data } = await queryFulfilled
                    dispatch(setTestResult(data))
                } catch(e) {
                    dispatch(setTestResult(null))
                    console.log(e)
                }
            }
        }),
        createTest: builder.mutation<number, ITest>({
            query: test => ({
                url: `${ApiTags.Test}`,
                method: HttpMethod.POST,
                body: test,
            }),
            invalidatesTags: () => [
                { type: ApiTags.Test }
            ],
        }),
        updateTest: builder.mutation<number, ITest>({
            query: test => ({
                url: `${ApiTags.Test}`,
                method: HttpMethod.PUT,
                body: test,
            }),
            invalidatesTags: id => [
                { type: ApiTags.Test }
            ],
        }),
        deleteTest: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Test}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                { type: ApiTags.Test }
            ],
        }),
    }),
})

export const {
    useGetTestsQuery,
    useGetTestQuery,
    useCheckTestMutation,
    useCreateTestMutation,
    useUpdateTestMutation,
    useDeleteTestMutation,
} = testApi