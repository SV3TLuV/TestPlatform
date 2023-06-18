import {useTypedSelector} from "../../hooks/redux";
import {Dialog} from "@mui/material";
import {Card, CloseButton, Col, Row} from "react-bootstrap";
import {TestEditForm} from "./TestEditForm";

interface ICreateTestDialogProps {
    open: boolean
    onClose: () => void
}

export const CreateTestDialog = ({open, onClose}: ICreateTestDialogProps) => {
    const {response} = useTypedSelector(state => state.auth)

    const onSave = () => {
        onClose()
    }

    return (
        <Dialog open={open}>
            <Card>
                <Card.Header className="text-center">
                    <Row>
                        <Col xs={11}>
                            <h3>Добавление теста</h3>
                        </Col>
                        <Col xs={1}>
                            <CloseButton onClick={onClose}/>
                        </Col>
                    </Row>
                </Card.Header>
                <Card.Body>
                    <TestEditForm
                        test={{
                            id: 0,
                            name: "",
                            description: "",
                            userId: response!.user!.id,
                            questions: [],
                        }}
                        type="create"
                        onSave={onSave}
                    />
                </Card.Body>
            </Card>
        </Dialog>
    )
}