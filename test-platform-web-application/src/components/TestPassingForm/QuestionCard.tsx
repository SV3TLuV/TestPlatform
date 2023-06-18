import {Card} from "react-bootstrap";
import {IQuestion} from "../../models/IQuestion";
import {AnswersFieldArray} from "./AnswersFieldArray";


interface IQuestionCardProps {
    nestIndex: any
    errors: any
    control: any
    visible: boolean
    question: IQuestion
}

export const QuestionCard = ({question, control, visible, nestIndex, errors}: IQuestionCardProps) => {
    return (
        <Card
            style={visible
                ? { }
                : { display: "none" }
            }
        >
            <Card.Header className="text-center">
                <h5>{question.text}</h5>
            </Card.Header>
            <Card.Body
                className="d-flex flex-column mx-auto"
                style={{ padding: "10px" }}
            >
                <AnswersFieldArray
                    nestIndex={nestIndex}
                    control={control}
                    errors={errors}
                />
            </Card.Body>
        </Card>
    )
}