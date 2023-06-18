import {useGetTestsQuery} from "../services/testApi";
import {Card, Container, Row, Form, Col, Button} from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import {TestCard} from "../components/TestCard";
import {MDBPagination, MDBPaginationItem, MDBPaginationLink} from "mdb-react-ui-kit";
import {Loading} from "../components/Loading";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {TestResultDialog} from "../components/TestResultDialog";
import {useDialog} from "../hooks/useDialog";
import {useTypedSelector} from "../hooks/redux";


export const TestsPage = () => {
    const [page, setPage] = useState(1)
    const [search, setSearch] = useState("")
    const [searchedText, setSearchedText] = useState<string | undefined>(undefined)
    const {data: list} = useGetTestsQuery({ page, pageSize: 24, searchedText })
    const {result} = useTypedSelector(state => state.testResult)
    const resultDialog = useDialog()
    const navigate = useNavigate()

    useEffect(() => {
        if (result) {
            resultDialog.handleOpen()
        }
    }, [result])

    useEffect(() => {
        if (search === "") {
            setSearchedText(undefined)
        }
    }, [search])

    if (!list) {
        return (
            <Loading/>
        )
    }

    const toTest = (id: number) => {
        navigate(`/tests/${id}`)
    }

    const onSearch = () => {
        setSearchedText(search)
    }

    const toNextPage = () => {
        setPage(value => value < list.totalPages ? value + 1 : value)
    }

    const toPreviousPage = () => {
        setPage(value => value > 1 ? value - 1 : value)
    }

    return (
        <>
            <TestResultDialog
                open={resultDialog.open}
                onClose={resultDialog.handleClose}
            />
            <Container style={{ padding: "20px" }}>
                <Row>
                    <Card
                        bg="light"
                        border="none"
                        style={{
                            margin: "10px",
                            width: "calc(100% - 20px)",
                        }}
                    >
                        <Card.Body>
                            <div className="d-flex">
                                <Row style={{ width: "100%" }}>
                                    <Col xs={6} sm={8} md={9} lg={10}>
                                        <Form.Control
                                            type="text"
                                            placeholder="Поиск"
                                            value={search}
                                            onChange={event => setSearch(event.target.value)}
                                        />
                                    </Col>
                                    <Col xs={6} sm={4} md={3} lg={2}>
                                        <Button onClick={onSearch} variant="primary" style={{ width: "100%" }}>
                                            Искать
                                        </Button>
                                    </Col>
                                </Row>
                            </div>
                        </Card.Body>
                    </Card>
                </Row>
                <Row>
                    {list.items.map(test => (
                        <Col
                            xs={12}
                            md={4}
                            key={test.id}
                            style={{
                                padding: "10px",
                                margin: "0px"
                            }}
                        >
                            <div
                                onClick={() => toTest(test.id)}
                                style={{ height: "100%" }}
                            >
                                <TestCard test={test} />
                            </div>
                        </Col>
                    ))}
                </Row>
                <Row>
                    <Card
                        className="align-items-center"
                        style={{
                            width: "calc(100% - 20px)",
                            margin: "10px",
                            padding: "5px"
                        }}
                    >
                        <MDBPagination className="mb-0 user-select-none">
                            <MDBPaginationItem>
                                <MDBPaginationLink onClick={toPreviousPage} aria-label='Previous'>
                                    <span aria-hidden='true'>«</span>
                                </MDBPaginationLink>
                            </MDBPaginationItem>
                            <MDBPaginationItem>
                                <MDBPaginationLink onClick={toPreviousPage} aria-label='Previous'>
                                    {page}
                                </MDBPaginationLink>
                            </MDBPaginationItem>
                            <MDBPaginationItem>
                                <MDBPaginationLink onClick={toNextPage} aria-label='Next'>
                                    <span aria-hidden='true'>»</span>
                                </MDBPaginationLink>
                            </MDBPaginationItem>
                        </MDBPagination>
                    </Card>
                </Row>
            </Container>
        </>
    )
}
