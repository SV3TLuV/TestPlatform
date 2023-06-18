import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {IAuthResponse} from "../../models/IAuthResponse";

interface IAuthState {
    response: IAuthResponse | null,
    isAuthorized: boolean
}

const authSlice = createSlice({
    name: "auth",
    initialState: {
        response: null,
        isAuthorized: false
    } as IAuthState,
    reducers: {
        setAuthState(state, action: PayloadAction<IAuthResponse | null>) {
            state.response = action.payload
            state.isAuthorized = action.payload !== null
        }
    }
})

export const {
    setAuthState
} = authSlice.actions

export default authSlice.reducer