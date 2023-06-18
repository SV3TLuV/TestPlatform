import {ApiTags, baseApi, buildUrlArguments} from "./baseApi";
import {HttpMethod} from "../common/HttpMethod";
import {IAnswer} from "../models/IAnswer";
import {IPagedList} from "../models/IPagedList";
import {IPaginatedQuery} from "../models/IPaginatedQuery";

export const answerApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getAnswers: builder.query<IPagedList<IAnswer>, IPaginatedQuery | void>({
            query: query => ({
                url: `${ApiTags.Answer}?${buildUrlArguments(query ?? {})}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result?.items ?? []).map(({id}) => ({type: ApiTags.Answer, id} as const)),
                {type: ApiTags.Answer, id: "LIST"},
            ],
        }),
        getAnswer: builder.query<IAnswer, number>({
            query: id => ({
                url: `${ApiTags.Answer}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.Answer, id},
            ]
        }),
        createAnswer: builder.mutation<number, IAnswer>({
            query: answer => ({
                url: `${ApiTags.Answer}`,
                method: HttpMethod.POST,
                body: answer,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Answer, id: id},
                {type: ApiTags.Answer, id: "LIST"},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
        updateAnswer: builder.mutation<number, IAnswer>({
            query: answer => ({
                url: `${ApiTags.Answer}/${answer.id}`,
                method: HttpMethod.PUT,
                body: answer,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Answer, id: id},
                {type: ApiTags.Answer, id: "LIST"},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
        deleteAnswer: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.Answer}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.Answer, id: id},
                {type: ApiTags.Answer, id: "LIST"},
                {type: ApiTags.Question, id: 'LIST'},
                {type: ApiTags.Test, id: 'LIST'},
            ],
        }),
    }),
})

export const {
    useGetAnswersQuery,
    useGetAnswerQuery,
    useCreateAnswerMutation,
    useUpdateAnswerMutation,
    useDeleteAnswerMutation
} = answerApi