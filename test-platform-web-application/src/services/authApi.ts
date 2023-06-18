import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/dist/query/react";
import {ILoginCommand} from "../models/ILoginCommand";
import {setAuthState} from "../redux/slices/authSlice";
import {IAuthResponse} from "../models/IAuthResponse";
import {ITokenPair} from "../models/ITokenPair";
import {APP_URL} from "../AppConfiguration";

export const authApi = createApi({
    reducerPath: "AuthApi",
    baseQuery: fetchBaseQuery({
        baseUrl: APP_URL,
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5173"
        },
    }),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({
        login: builder.mutation<IAuthResponse, ILoginCommand>({
            query: command => ({
                url: "auth/login",
                method: "POST",
                body: command
            }),
            async onQueryStarted(arg, {dispatch, queryFulfilled}) {
                try {
                    const { data } = await queryFulfilled
                    dispatch(setAuthState(data))
                } catch(e) {
                    dispatch(setAuthState(null))
                    console.log(e)
                }
            }
        }),
        refresh: builder.mutation<IAuthResponse, ITokenPair>({
            query: query => ({
                url: "auth/refresh",
                method: "POST",
                body: query
            }),
            async onQueryStarted(arg, {dispatch, queryFulfilled}) {
                try {
                    const { data } = await queryFulfilled
                    dispatch(setAuthState(data))
                } catch(e) {
                    dispatch(setAuthState(null))
                    console.log(e)
                }
            }
        }),
    }),
})

export const {
    useLoginMutation,
    useRefreshMutation
} = authApi