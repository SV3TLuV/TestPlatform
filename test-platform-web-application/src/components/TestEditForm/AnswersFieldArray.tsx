import {Controller, useFieldArray, useWatch} from "react-hook-form";
import {Button, Card, Col, Form, Row} from "react-bootstrap";
import {IQuestion} from "../../models/IQuestion";
import {IAnswer} from "../../models/IAnswer";
import {ValidationMessages} from "../../common/ValidationMessages";
import {EditableAnswer} from "./EditableAnswer";


interface IAnswersFieldArrayProps {
    nestIndex: any
    control: any
    errors: any
    question: IQuestion
}

export const AnswersFieldArray = ({nestIndex, control, errors, question}: IAnswersFieldArrayProps) => {
    const { fields, append, remove } = useFieldArray({
        control, name: `questions[${nestIndex}].answers`
    })
    const answers: IAnswer[] = useWatch({
        control, name: `questions[${nestIndex}].answers`
    })

    const handleAdd = () => {
        if (!answers) return;

        const newId = answers.length !== 0
            ? Math.max(...answers.map(a => a.id)) + 1
            : 1;

        append({
            id: newId,
            text: "",
            questionId: question.id,
            testId: question.testId,
            isRight: false
        })
    }

    return (
        <Card>
            <Card.Header className="text-center">
                <Card.Title>
                    Ответы
                </Card.Title>
            </Card.Header>
            <Card.Body>
                {fields.map((item, index) => (
                    <Row key={item.id} style={{ marginBottom: "10px" }}>
                        <EditableAnswer
                            questionIndex={nestIndex}
                            index={index}
                            control={control}
                            errors={errors}
                            onDelete={remove}
                        />
                    </Row>
                ))}
            </Card.Body>
            <Card.Footer>
                <Button onClick={handleAdd}>
                    Добавить ответ
                </Button>
            </Card.Footer>
        </Card>
    )
}