import {ITest} from "../../models/ITest";
import {Button, Card, Col, Container, Form, Row} from "react-bootstrap";
import {useState} from "react";
import {QuestionCard} from "./QuestionCard";
import {SubmitHandler, useFieldArray, useForm, useFormState} from "react-hook-form";
import {IQuestion} from "../../models/IQuestion";
import {useCheckTestMutation} from "../../services/testApi";
import {useAppDispatch} from "../../hooks/redux";
import {setTestResult} from "../../redux/slices/testResultState";
import {useNavigate} from "react-router-dom";
import {useDialog} from "../../hooks/useDialog";
import {ErrorDialog} from "../ErrorDialog";


interface ITestPassingFormProps {
    test: ITest
}

export const TestPassingForm = ({test}: ITestPassingFormProps) => {
    const [check] = useCheckTestMutation()
    const errorDialog = useDialog()
    const navigate = useNavigate()
    const {control, handleSubmit, getValues} = useForm<ITest>({ mode: "onChange", values: test })
    const {errors} = useFormState({ control })

    const { fields } = useFieldArray({
        control, name: "questions"
    })

    const [index, setIndex] = useState(0)

    const onSubmit:SubmitHandler<ITest> = async (data: ITest) => {
        const result = await check(data)

        if ("data" in result && result.data) {
            navigate("/tests")
        }
    }

    const isCorrect = (question: IQuestion): boolean => {
        return question.answers.some(answer => answer.isRight)
    }

    const nextQuestion = () => {
        if (!test) return;
        if (isCorrect(getValues("questions")[index])) {
            const max = test.questions.length - 1
            setIndex(value => value < max ? value + 1 : value)
        } else {
            errorDialog.handleOpen()
        }
    }

    const previousQuestion = () => {
        if (!test) return;
        setIndex(value => value > 0 ? value - 1 : value)
    }

    return (
        <>
            <ErrorDialog
                open={errorDialog.open}
                onClose={errorDialog.handleClose}
                message={"Нужно выбрать один или несколько ответов"}
            />
            <Container>
                <Form onSubmit={handleSubmit(onSubmit)}>
                    <Card>
                        <Card.Header className="text-center">
                            <Card.Title>
                                {test.name}
                            </Card.Title>
                        </Card.Header>
                        <Card.Body>
                            <Card.Text>
                                {test.description}
                            </Card.Text>
                        </Card.Body>
                        <Card.Footer>
                            <Row>
                                <Col md={8} style={{ marginBottom: "10px" }}>
                                    {fields.map((item, qIndex) => (
                                        <QuestionCard
                                            key={item.id}
                                            errors={errors}
                                            control={control}
                                            nestIndex={qIndex}
                                            visible={index === qIndex}
                                            question={test.questions[qIndex]}
                                        />
                                    ))}
                                </Col>
                                <Col md={4} style={{ marginBottom: "10px" }}>
                                    <Row>
                                        <Col>
                                            <Card style={{ marginBottom: "10px" }}>
                                                <Card.Body
                                                    className="text-center"
                                                    style={{ padding: "10px" }}
                                                >
                                                    <span>Вопрос {index + 1} из {test.questions.length}</span>
                                                </Card.Body>
                                            </Card>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col>
                                            <Card style={{ marginBottom: "10px" }}>
                                                <Card.Body
                                                    style={{
                                                        padding: "10px",
                                                        paddingTop: "18px",
                                                        paddingBottom: "12px",
                                                    }}
                                                >
                                                    <Row>
                                                        <Col style={{ marginBottom: "10px" }}>
                                                            <Button
                                                                onClick={previousQuestion}
                                                                style={{
                                                                    width: "100%",
                                                                    height: "100%",
                                                                }}
                                                            >
                                                                Предыдущий вопрос
                                                            </Button>
                                                        </Col>
                                                        <Col style={{ marginBottom: "10px" }}>
                                                            <Button
                                                                onClick={nextQuestion}
                                                                style={{
                                                                    width: "100%",
                                                                    height: "100%",
                                                                }}
                                                            >
                                                                Следующий вопрос
                                                            </Button>
                                                        </Col>
                                                    </Row>
                                                </Card.Body>
                                            </Card>
                                        </Col>
                                    </Row>
                                    <Row>
                                        <Col>
                                            <Card>
                                                <Card.Body style={{ padding: "16px", paddingBottom: "6px" }}>
                                                    <Button
                                                        type="submit"
                                                        style={{
                                                            width: "100%",
                                                            height: "100%",
                                                        }}
                                                        variant="outline-primary"
                                                    >
                                                        Закончить
                                                    </Button>
                                                </Card.Body>
                                            </Card>
                                        </Col>
                                    </Row>
                                </Col>
                            </Row>
                        </Card.Footer>
                    </Card>
                </Form>
            </Container>
        </>
    )
}