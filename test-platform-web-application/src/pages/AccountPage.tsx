import {Card, Col, Container, Row} from "react-bootstrap";
import {AccountCard} from "../components/AccountCard";
import {UserTestList} from "../components/UserTestList";
import {useTypedSelector} from "../hooks/redux";
import {useDialog} from "../hooks/useDialog";


export const AccountPage = () => {
    const {response} = useTypedSelector(state => state.auth)

    return (
        <Container
            style={{
                padding: "40px",
                height: "calc(100vh - 80px)"
            }}
        >
            <Row>
                <Col
                    xs={12} md={4}
                    style={{ marginBottom: "20px" }}
                >
                    <AccountCard user={response!.user}/>
                </Col>
                <Col
                    xs={12} md={8}
                    style={{ marginBottom: "20px" }}
                >
                    <Card>
                        <Card.Header className="text-center">
                            <Card.Title>
                                Мои тесты
                            </Card.Title>
                        </Card.Header>
                        <Card.Body style={{ padding: "10px", height: "400px" }}>
                            <UserTestList userId={response!.user!.id}/>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </Container>
    )
}