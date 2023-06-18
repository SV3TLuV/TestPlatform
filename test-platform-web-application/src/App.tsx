import {NavBar} from "./components/NavBar";
import {createBrowserRouter, createRoutesFromElements, Outlet, Route, RouterProvider} from "react-router-dom";
import React from "react";
import {LoginPage} from "./pages/LoginPage";
import {RegistrationPage} from "./pages/RegistrationPage";
import {TestsPage} from "./pages/TestsPage";
import {Provider} from "react-redux";
import {persistor, store} from "./redux/store";
import {PersistGate} from "redux-persist/integration/react";
import './styles/App.css'
import 'bootstrap/dist/css/bootstrap.min.css';
import 'mdb-react-ui-kit/dist/css/mdb.min.css';
import "@fortawesome/fontawesome-free/css/all.min.css";
import {MainPage} from "./pages/MainPage";
import {AccountPage} from "./pages/AccountPage";
import {RequireAuth} from "./hok/RequireAuth";
import {TestPage} from "./pages/TestPage";


export default function App() {
    return (
        <Provider store={store}>
            <PersistGate loading={null} persistor={persistor}>
                <RouterProvider router={router}/>
            </PersistGate>
        </Provider>
    )
}

const Root = () => {
    return (
        <div className="Root">
            <Outlet/>
        </div>
    )
}

export const router = createBrowserRouter(
    createRoutesFromElements(
        <Route path="/" element={<Root/>}>
            <Route path="login" element={<LoginPage/>}/>
            <Route path="registration" element={<RegistrationPage/>}/>
            <Route path="/" element={
                <>
                    <NavBar/>
                    <MainPage/>
                </>
            }/>
            <Route path="account" element={
                <RequireAuth>
                    <NavBar/>
                    <AccountPage/>
                </RequireAuth>
            }/>
            <Route path="tests" element={
                <>
                    <NavBar/>
                    <TestsPage/>
                </>
            }/>
            <Route path="tests/:id" element={
                <>
                    <NavBar/>
                    <TestPage/>
                </>
            }/>
        </Route>
    )
)