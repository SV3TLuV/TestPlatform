import {Card, Form, Row} from "react-bootstrap";
import {Controller, FieldError} from "react-hook-form";
import {ValidationMessages} from "../../common/ValidationMessages";
import {AnswersFieldArray} from "./AnswersFieldArray";
import {IQuestion} from "../../models/IQuestion";
import {MdDeleteForever} from "react-icons/all";
import {IconButton} from "@mui/material";
import lget from "lodash.get";
import {QuestionTextValidation} from "./TestEditFormValidation";

interface IEditableQuestionProps {
    control: any
    errors: any
    index: number
    onDelete: (index: number) => void
    question: IQuestion
}

export const EditableQuestion = (
    {
        control,
        errors,
        index,
        onDelete,
        question,
    }: IEditableQuestionProps) => {

    const handleDelete = () => onDelete(index)

    return (
        <Card>
            <Card.Header
                className="text-end"
                style={{ padding: "5px" }}
            >
                <Card.Title>
                    <span>
                        {`Вопрос ${index + 1}`}
                    </span>
                    <IconButton onClick={handleDelete}>
                        <MdDeleteForever/>
                    </IconButton>
                </Card.Title>
            </Card.Header>
            <Card.Body style={{ padding: "10px" }}>
                <Row style={{ marginBottom: "10px" }}>
                    <Controller
                        name={`questions.${index}.text`}
                        control={control}
                        rules={QuestionTextValidation}
                        render={({ field }) => (
                            <Form.Group>
                                <Form.Control
                                    placeholder="Вопрос"
                                    as="textarea"
                                    rows={3}
                                    value={field.value}
                                    onChange={field.onChange}
                                    style={{ resize: "none" }}
                                />
                                {errors && (
                                    <Form.Text className="text-danger">
                                        {(lget(errors, `questions.[${index}].text`) as FieldError)?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                </Row>
                <Row>
                    <AnswersFieldArray
                        control={control}
                        nestIndex={index}
                        errors={errors}
                        question={question}
                    />
                </Row>
            </Card.Body>
        </Card>
    )
}