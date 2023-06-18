import {Container, Nav, Navbar, Button, NavDropdown} from "react-bootstrap";
import {BsFillQuestionSquareFill, SlMenu, VscAccount} from "react-icons/all";
import {useTypedSelector} from "../hooks/redux";
import React from "react";
import {useNavigate} from "react-router-dom";

export const NavBar = () => {
    const {isAuthorized, response} = useTypedSelector(state => state.auth)
    const navigate = useNavigate()

    const toLogin = () => navigate("/login")

    const toAccount = () => navigate("/account")

    const toTests = () => navigate("/tests")

    return (
        <Navbar bg="light" expand="md" className="py-3" style={{ boxShadow: "0px -8px 20px 7px" }}>
            <Container>
                <Navbar.Brand href="/" className="navbar-brand d-flex align-items-center">
                    <span className="bs-icon-sm bs-icon-rounded bs-icon-primary d-flex justify-content-center align-items-center me-2 bs-icon">
                        <BsFillQuestionSquareFill/>
                    </span>
                    <span className="fw-bolder">
                        <strong>Quizzy</strong>
                    </span>
                </Navbar.Brand>
                <Navbar.Toggle aria-controls="navcol-2">
                    <span className="visually-hidden">Toggle navigation</span>
                    <SlMenu/>
                </Navbar.Toggle>
                <Navbar.Collapse id="navcol-2">
                    <Nav className="me-auto">
                        <Nav.Link onClick={toTests} active>
                            Тесты
                        </Nav.Link>
                    </Nav>
                    {isAuthorized
                        ?
                        <Nav>
                            <Nav.Link onClick={toAccount} active>
                                <VscAccount size="2em"/>
                                <span style={{ padding: "10px"}}>
                                    {response!.user!.login}
                                </span>
                            </Nav.Link>
                        </Nav>
                        :
                        <Button onClick={toLogin} className="ms-md-2" variant="primary" role="button">
                            Войти
                        </Button>
                    }
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}
