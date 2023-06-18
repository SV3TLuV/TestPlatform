import { Navigate } from "react-router-dom";
import {useTypedSelector} from "../hooks/redux";


interface IRequireAuthProps {
    children: any
}

export const RequireAuth = ({ children }: IRequireAuthProps) => {
    const { isAuthorized } = useTypedSelector(state => state.auth)

    if (!isAuthorized) {
        return <Navigate to="/login" />
    }

    return children;
}