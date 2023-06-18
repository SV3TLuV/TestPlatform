import {useAppDispatch, useTypedSelector} from "../hooks/redux";
import {Dialog} from "@mui/material";
import {Card, CloseButton, Col, Row} from "react-bootstrap";
import {setTestResult} from "../redux/slices/testResultState";
import {useGetTestQuery} from "../services/testApi";
import {BsEmojiAngry, BsEmojiSmile, RiEmotionNormalLine} from "react-icons/all";


interface ITestResultDialogProps {
    open: boolean
    onClose: () => void
}

export const TestResultDialog = ({open, onClose}: ITestResultDialogProps) => {
    const {result} = useTypedSelector(state => state.testResult)
    const {response} = useTypedSelector(state => state.auth)
    const {data: test} = useGetTestQuery(result?.testId ?? 0, {
        skip: !result
    })
    const dispatch = useAppDispatch()

    const handleClose = () => {
        dispatch(setTestResult(null))
        onClose()
    }

    if (!test || !result) {
        return (
            <></>
        )
    }

    const message = (function () {
        if (result.percent >= 90)
            return "Отлично"
        if (result.percent >= 80)
            return "Могло бы быть и лучше"
        return "Вам стоит поднянуть свои знания"
    })()

    const icon = (function () {
        if (result.percent >= 100)
            return <BsEmojiSmile size="3em" color="green" />
        if (result.percent >= 80)
            return <RiEmotionNormalLine size="3.5em" color="#FFCF48" />
        return <BsEmojiAngry size="3em" color="red" />
    })()

    return (
        <Dialog open={open}>
            <Card className="text-center">
                <Card.Header>
                    <Row>
                        <Col xs={10} className="text-start">
                            <Card.Title>
                                Результат
                            </Card.Title>
                        </Col>
                        <Col xs={2}>
                            <CloseButton onClick={handleClose}/>
                        </Col>
                    </Row>
                </Card.Header>
                <Card.Body>
                    <h6>{test?.name}</h6>
                    <Card.Text>
                        {icon}
                    </Card.Text>
                    <Card.Text>
                        {response && <h5>{response.user.login}</h5>}
                        {message}
                    </Card.Text>
                    <Card.Text>
                        {`Ваш результат: ${result.percent}%`}
                    </Card.Text>
                </Card.Body>
                <Card.Footer>
                    <Card.Text>
                        {`${result.countRightAnswers} правильных ответов из ${result.countAnswers}`}
                    </Card.Text>
                </Card.Footer>
            </Card>
        </Dialog>
    )
}