import {Dialog} from "@mui/material";
import {Card, CloseButton, Col, Row} from "react-bootstrap";
import {BiErrorCircle} from "react-icons/all";


interface IErrorDialogProps {
    open: boolean
    onClose: () => void
    message: string
}

export const ErrorDialog = ({open, onClose, message}: IErrorDialogProps) => {
    return (
        <Dialog open={open}>
            <Card className="text-center">
                <Card.Header>
                    <Row>
                        <Col xs={10}>
                            <Card.Title className="text-start">
                                Внимание
                            </Card.Title>
                        </Col>
                        <Col xs={2}>
                            <CloseButton onClick={onClose}/>
                        </Col>
                    </Row>
                </Card.Header>
                <Card.Body>
                    <Card.Text>
                        <BiErrorCircle
                            size="3em"
                            color="red"
                        />
                    </Card.Text>
                    <Card.Text>
                        {message}
                    </Card.Text>
                </Card.Body>
            </Card>
        </Dialog>
    )
}