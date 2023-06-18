import {Card, Container} from "react-bootstrap";
import {ITest} from "../models/ITest";
import {useGetUserQuery} from "../services/userApi";

interface ITestCardProps {
    test: ITest
}

export const TestCard = ({test}: ITestCardProps) => {
    const {data: author} = useGetUserQuery(test.userId)

    return (
        <Card
            className="hover-overlay user-select-none"
            style={{ height: "100%" }}
        >
            <Card.Header>
                <Card.Title>
                    {test.name}
                </Card.Title>
            </Card.Header>
            <Card.Body>
                <Card.Text>
                    {test.description}
                </Card.Text>
            </Card.Body>
            <Card.Footer className="text-muted text-end">
                {author?.login}
            </Card.Footer>
            <div
                className="mask rounded-4"
                style={{
                    background: "linear-gradient(45deg, rgba(29, 236, 197, 0.2), rgba(91, 14, 214, 0.5) 100%)",
                }}
            />
        </Card>
    )
}