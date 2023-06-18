import {ITest} from "../../models/ITest";
import {Dialog} from "@mui/material";
import {Card, CloseButton, Col, Row} from "react-bootstrap";
import {TestEditForm} from "./TestEditForm";


interface IUpdateTestDialogProps {
    test: ITest
    open: boolean
    onClose: () => void
}

export const UpdateTestDialog = ({test, open, onClose}: IUpdateTestDialogProps) => {
    const onSave = () => {
        onClose()
    }

    return (
        <Dialog open={open}>
            <Card>
                <Card.Header className="text-center">
                    <Row>
                        <Col xs={11}>
                            <h3>Изменение теста</h3>
                        </Col>
                        <Col xs={1}>
                            <CloseButton onClick={onClose}/>
                        </Col>
                    </Row>
                </Card.Header>
                <Card.Body>
                    <TestEditForm
                        test={test}
                        type="edit"
                        onSave={onSave}
                    />
                </Card.Body>
            </Card>
        </Dialog>
    )
}
