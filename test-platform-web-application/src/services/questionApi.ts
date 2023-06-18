import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "../common/HttpMethod";
import {IQuestion} from "../models/IQuestion";
import {IPagedList} from "../models/IPagedList";
import {IPaginatedQuery} from "../models/IPaginatedQuery";

export const questionApi = baseApi.injectEndpoints({
    endpoints: build => ({
        getQuestions: build.query<IPagedList<IQuestion>, IPaginatedQuery | void>({
            query: query => ({
                url: `${ApiTags.Question}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Question, id} as const)),
                { type: ApiTags.Question, id: 'LIST' },
            ],
        }),
        getQuestion: build.query<IQuestion, number>({
            query: id => ({
                url: `${ApiTags.Question}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Question, id}
            ],
        }),
        createQuestion: build.mutation<number, IQuestion>({
            query: question => ({
                url: ApiTags.Question,
                method: HttpMethod.POST,
                body: question,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Question, id: id},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
        updateQuestion: build.mutation<number, IQuestion>({
            query: question => ({
                url: `${ApiTags.Question}/${question.id}`,
                method: HttpMethod.PUT,
                body: question,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Question, id: id},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
        deleteQuestion: build.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Question}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Question, id: id},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
    }),
})

export const {
    useGetQuestionsQuery,
    useGetQuestionQuery,
    useCreateQuestionMutation,
    useUpdateQuestionMutation,
    useDeleteQuestionMutation,
} = questionApi;