import {Form, Row} from "react-bootstrap";
import {Controller, FieldError, useFieldArray, useFormState, useWatch} from "react-hook-form";
import {IAnswer} from "../../models/IAnswer";
import lget from "lodash.get";


interface IAnswersFieldArrayProps {
    nestIndex: number
    control: any
    errors: any
}

export const AnswersFieldArray = ({ nestIndex, control, errors }: IAnswersFieldArrayProps) => {
    const { fields } = useFieldArray({
        control, name: `questions[${nestIndex}].answers`
    })
    const answers: IAnswer[] = useWatch({
        control, name: `questions[${nestIndex}].answers`
    })

    return (
        <>
            {fields.map((item, index) => (
                <Row key={item.id} style={{ marginBottom: "10px" }}>
                    <Controller
                        name={`questions.${nestIndex}.answers.${index}.isRight`}
                        control={control}
                        render={({ field }) => (
                            <Form.Group>
                                <Form.Check
                                    type="checkbox"
                                    label={answers[index].text}
                                    checked={field.value}
                                    onChange={field.onChange}
                                />
                                {errors && (
                                    <Form.Text className="text-danger">
                                        {(lget(errors, `questions.${nestIndex}.answers.${index}.isRight`) as FieldError)?.message}
                                    </Form.Text>
                                )}
                            </Form.Group>
                        )}
                    />
                </Row>
            ))}
        </>
    )
}