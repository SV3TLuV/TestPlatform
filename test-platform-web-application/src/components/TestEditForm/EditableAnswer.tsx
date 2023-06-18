import {Card, Form, Row} from "react-bootstrap";
import {Controller, FieldError} from "react-hook-form";
import {ValidationMessages} from "../../common/ValidationMessages";
import {IconButton} from "@mui/material";
import {MdDeleteForever} from "react-icons/all";
import lget from "lodash.get";
import {AnswerTextValidation} from "./TestEditFormValidation";


interface IEditableAnswerProps {
    control: any
    errors: any
    index: number
    onDelete: (index: number) => void
    questionIndex: number
}

export const EditableAnswer = (
    {
        questionIndex,
        index,
        control,
        errors,
        onDelete
    }: IEditableAnswerProps) => {

    const handleDelete = () => onDelete(index)

    return (
        <Card>
            <Card.Header
                className="text-end"
                style={{ padding: "5px" }}
            >
                <IconButton onClick={handleDelete}>
                    <MdDeleteForever/>
                </IconButton>
            </Card.Header>
            <Card.Body>
                <Row style={{ marginBottom: "15px" }}>
                    <Controller
                        name={`questions.${questionIndex}.answers.${index}.text`}
                        control={control}
                        rules={AnswerTextValidation}
                        render={({ field }) => (
                            <Form.Group>
                                <Form.Control
                                    placeholder="Ответ"
                                    as="textarea"
                                    rows={4}
                                    value={field.value}
                                    onChange={field.onChange}
                                    style={{ resize: "none" }}
                                />
                                {errors && (
                                    <Form.Text className="text-danger">
                                        {(lget(errors, `questions.${questionIndex}.answers.${index}.text`) as FieldError)?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                </Row>
                <Row>
                    <Controller
                        name={`questions.${questionIndex}.answers.${index}.isRight`}
                        control={control}
                        render={({ field }) => (
                            <Form.Group className="text-start">
                                <Form.Check
                                    type="checkbox"
                                    label="Правильный ответ"
                                    checked={field.value}
                                    onChange={field.onChange}
                                />
                                {errors && (
                                    <Form.Text className="text-danger">
                                        {(lget(errors, `questions.${questionIndex}.answers.${index}.isRight`) as FieldError)?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                </Row>
            </Card.Body>
        </Card>
    )
}