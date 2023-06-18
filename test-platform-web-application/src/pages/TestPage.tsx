import {useParams} from "react-router-dom";
import { Centered } from "../components/Centered";
import {useGetTestQuery} from "../services/testApi";
import {Loading} from "../components/Loading";
import {TestPassingForm} from "../components/TestPassingForm/TestPassingForm";
import {useDialog} from "../hooks/useDialog";
import {ErrorDialog} from "../components/ErrorDialog";

export const TestPage = () => {
    const { id } = useParams<"id">()
    const {data: test} = useGetTestQuery(parseInt(id ? id : "0"))

    if (!test) {
        return (
            <Loading/>
        )
    }

    return (
        <Centered>
            <TestPassingForm test={test} />
        </Centered>
    )
}