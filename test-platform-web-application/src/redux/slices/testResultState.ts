import {createSlice, PayloadAction} from "@reduxjs/toolkit";
import {ITestResult} from "../../models/ITestResult";

interface ITestResultState {
    result: ITestResult | null
}

export const testResultState = createSlice({
    name: "testResult",
    initialState: {
        result: null
    } as ITestResultState,
    reducers: {
        setTestResult: (state, action: PayloadAction<ITestResult | null>) => {
            state.result = action.payload
        }
    }
})

export const {
    setTestResult
} = testResultState.actions

export default testResultState.reducer