import {ApiTags, baseApi} from "./baseApi";
import {IUser} from "../models/IUser";
import {HttpMethod} from "../common/HttpMethod";
import {IRegisterCommand} from "../models/IRegisterCommand";
import {ITest} from "../models/ITest";

export const userApi = baseApi.injectEndpoints({
    endpoints: builder => ({
        getUsers: builder.query<IUser[], void>({
            query: () => ({
                url: `${ApiTags.User}`,
                method: HttpMethod.GET,
            }),
            providesTags: result => [
                ...(result ?? []).map(({id}) => ({type: ApiTags.User, id} as const)),
                {type: ApiTags.User, id: "LIST"},
            ]
        }),
        getUser: builder.query<IUser, number>({
            query: id => ({
                url: `${ApiTags.User}/${id}`,
                method: HttpMethod.GET,
            }),
            providesTags: (result, error, id) => [
                {type: ApiTags.User, id},
            ]
        }),
        getUserTest: builder.query<ITest[], number>({
            query: id => ({
                url: `${ApiTags.User}/${id}/Tests`,
                method: HttpMethod.GET,
            }),
            providesTags: () => [
                { type: ApiTags.Test }
            ]
        }),
        createUser: builder.mutation<number, IRegisterCommand>({
            query: command => ({
                url: `${ApiTags.User}`,
                method: HttpMethod.POST,
                body: command,
            }),
            invalidatesTags: id => [
                {type: ApiTags.User, id: id}
            ],
        }),
        updateUser: builder.mutation<number, IUser>({
            query: user => ({
                url: `${ApiTags.User}/${user.id}`,
                method: HttpMethod.PUT,
                body: user,
            }),
            invalidatesTags: id => [
                {type: ApiTags.User, id: id}
            ],
        }),
        deleteUser: builder.mutation<number, number>({
            query: id => ({
                url: `${ApiTags.User}/${id}`,
                method: HttpMethod.DELETE,
            }),
            invalidatesTags: id => [
                {type: ApiTags.User, id: id}
            ],
        }),
    }),
})

export const {
    useGetUsersQuery,
    useGetUserQuery,
    useGetUserTestQuery,
    useCreateUserMutation,
    useUpdateUserMutation,
    useDeleteUserMutation,
} = userApi