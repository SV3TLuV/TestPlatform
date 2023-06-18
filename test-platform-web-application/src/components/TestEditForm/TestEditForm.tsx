import {Button, Card, Col, Container, Form, Row} from "react-bootstrap";
import {ITest} from "../../models/ITest";
import {Controller, SubmitHandler, useFieldArray, useForm, useFormState} from "react-hook-form";
import {ValidationMessages} from "../../common/ValidationMessages";
import {AnswersFieldArray} from "./AnswersFieldArray";
import {EditableQuestion} from "./EditableQuestion";
import {useCreateTestMutation, useUpdateTestMutation} from "../../services/testApi";
import {useNavigate} from "react-router-dom";
import {useDialog} from "../../hooks/useDialog";
import {ErrorDialog} from "../ErrorDialog";
import {useState} from "react";
import {TestDescriptionValidation, TestNameValidation} from "./TestEditFormValidation";


interface ITestEditFormProps {
    test: ITest
    type: TestEditFormType
    onSave: () => void
}

export type TestEditFormType = "create" | "edit";

export const TestEditForm = ({test, type, onSave}: ITestEditFormProps) => {
    const [create] = useCreateTestMutation()
    const [update] = useUpdateTestMutation()

    const {control, handleSubmit, watch, setError} = useForm<ITest>({
        mode: "onChange", values: test
    })
    const {errors} = useFormState({ control })
    const questions = watch("questions")

    const { fields, append, remove } = useFieldArray({
        control, name: "questions"
    })

    const errorDialog = useDialog()
    const [message, setMessage] = useState("")

    const addQuestion = () => {
        if (!questions) return;

        const newId = questions.length !== 0
            ? Math.max(...questions.map(q => q.id)) + 1
            : 1;

        append({
            id: newId,
            testId: test.id,
            text: "",
            answers: []
        })
    }

    const onSubmit: SubmitHandler<ITest> = async (test) => {
        let hasError = false

        if (test.questions.length <= 0) {
            setMessage("Тест должен содержать хотя бы один вопрос")
            errorDialog.handleOpen()
            hasError = true
        }

        questions.forEach((question, questionIndex) => {
            const errors = []

            if (question.answers.length <= 1) {
                setMessage(`Вопрос ${questionIndex + 1} должен содержать хотя бы два ответа`)
                errorDialog.handleOpen()
                hasError = true
            }

            if (!question.answers.some(a => a.isRight)) {
                errors.push({
                    type: "manual",
                    message: "Вопрос должен иметь хотя бы один правильный ответ"
                })
                hasError = true
            }

            if (hasError) {
                errors.forEach(error => {
                    question.answers.forEach((answer, answerIndex) => {
                        setError(`questions.${questionIndex}.answers.${answerIndex}.isRight`, error)
                    })
                })
            }
        })

        if (!hasError) {
            const result = type === "create"
                ? await create(test)
                : await update(test)

            if ("error" in result && result.error) {
                return;
            }

            onSave()
        }
    }

    return (
        <>
            <ErrorDialog
                open={errorDialog.open}
                onClose={errorDialog.handleClose}
                message={message}
            />
            <Container style={{ maxWidth: "480px" }}>
                <Form onSubmit={handleSubmit(onSubmit)}>
                    <Row>
                        <Col
                            xs={12}
                            style={{ marginBottom: "20px" }}
                        >
                            <Card className="text-center">
                                <Card.Header>
                                    <Card.Title>
                                        Информация о тесте
                                    </Card.Title>
                                </Card.Header>
                                <Card.Body>
                                    <Row style={{ marginBottom: "10px" }}>
                                        <Controller
                                            name="name"
                                            control={control}
                                            rules={TestNameValidation}
                                            render={({ field }) => (
                                                <Form.Group>
                                                    <Form.Control
                                                        placeholder="Название"
                                                        value={field.value}
                                                        onChange={field.onChange}
                                                    />
                                                    {errors.name && (
                                                        <Form.Text className="text-danger">
                                                            {errors.name?.message}
                                                        </Form.Text>
                                                    )}
                                                </Form.Group>
                                            )}
                                        />
                                    </Row>
                                    <Row>
                                        <Controller
                                            name="description"
                                            control={control}
                                            rules={TestDescriptionValidation}
                                            render={({ field }) => (
                                                <Form.Group>
                                                    <Form.Control
                                                        placeholder="Описание"
                                                        as="textarea"
                                                        rows={4}
                                                        value={field.value}
                                                        onChange={field.onChange}
                                                        style={{ resize: "none" }}
                                                    />
                                                    {errors.description && (
                                                        <Form.Text className="text-danger">
                                                            {errors.description?.message}
                                                        </Form.Text>
                                                    )}
                                                </Form.Group>
                                            )}
                                        />
                                    </Row>
                                </Card.Body>
                            </Card>
                        </Col>
                        <Col
                            xs={12}
                            style={{ marginBottom: "20px" }}
                        >
                            <Card className="text-center">
                                <Card.Header>
                                    <Card.Title>
                                        Вопросы
                                    </Card.Title>
                                </Card.Header>
                                <Card.Body>
                                    {fields.map((item, index) => (
                                        <Row
                                            key={item.id}
                                            style={{ marginBottom: "20px" }}
                                        >
                                            <EditableQuestion
                                                control={control}
                                                errors={errors}
                                                index={index}
                                                onDelete={remove}
                                                question={questions[index]}
                                            />
                                        </Row>
                                    ))}
                                </Card.Body>
                                <Card.Footer>
                                    <Button onClick={addQuestion}>
                                        Добавить вопрос
                                    </Button>
                                </Card.Footer>
                            </Card>
                        </Col>
                        <Col xs={12} className="text-center">
                            <Card>
                                <Card.Body style={{ padding: "10px" }}>
                                    <Button type="submit">
                                        Сохранить
                                    </Button>
                                </Card.Body>
                            </Card>
                        </Col>
                    </Row>
                </Form>
            </Container>
        </>
    )
}