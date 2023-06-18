import {createApi, fetchBaseQuery} from "@reduxjs/toolkit/query/react";
import {RootState} from "../redux/store";
import {APP_URL} from "../AppConfiguration";

export enum ApiTags {
    User = "User",
    Test = "Test",
    Question = "Question",
    Answer = "Answer"
}

export const baseApi = createApi({
    reducerPath: "BaseApi",
    baseQuery: fetchBaseQuery({
        baseUrl: APP_URL,
        headers: {
            "Accept": "application/json",
            "Content-Type": "application/json",
            "Access-Control-Allow-Origin": "http://127.0.0.1:5173"
        },
        prepareHeaders: (headers, { getState }) => {
            const tokens = (getState() as RootState).auth.response?.tokens ?? null;
            if (tokens) {
                headers.set("Authorization", `Bearer ${tokens.accessToken}`)
            }
            return headers
        }
    }),
    tagTypes: Object.values(ApiTags),
    refetchOnReconnect: true,
    refetchOnFocus: true,
    endpoints: builder => ({}),
})

export const buildUrlArguments = (params: object): string => {
    const urlParams = new URLSearchParams()
    for (const [key, value] of Object.entries(params)) {
        if (value !== undefined && value !== null) {
            urlParams.append(key, value.toString())
        }
    }
    return urlParams.toString()
}

