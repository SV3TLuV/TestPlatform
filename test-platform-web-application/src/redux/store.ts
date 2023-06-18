import {combineReducers, configureStore} from "@reduxjs/toolkit";
import {persistReducer} from "redux-persist";
import storage from "redux-persist/es/storage";
import persistStore from "redux-persist/es/persistStore";
import {FLUSH, PAUSE, PERSIST, PURGE, REGISTER, REHYDRATE} from "redux-persist/es/constants";
import {baseApi} from "../services/baseApi";
import authReducer from "./slices/authSlice";
import testResultReducer from "./slices/testResultState";
import {authApi} from "../services/authApi";

const rootReducer = combineReducers({
    [baseApi.reducerPath]: baseApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
    auth: authReducer,
    testResult: testResultReducer
});

const persistedReducer = persistReducer({
    key: "root",
    storage,
    whitelist: ["auth"],
}, rootReducer);

export const setupStore = () => {
    return configureStore({
        reducer: persistedReducer,
        middleware: getDefaultMiddleware =>
            getDefaultMiddleware({
                serializableCheck: {
                    ignoredActions: [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]
                }
            }).concat(baseApi.middleware, authApi.middleware),
    });
}

export const store = setupStore()
export const persistor = persistStore(store)
export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']