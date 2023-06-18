import {Row, Button, Card} from "react-bootstrap";
import {VscAccount} from "react-icons/all";
import {useDeleteUserMutation} from "../services/userApi";
import {useNavigate} from "react-router-dom";
import {IUser} from "../models/IUser";
import {useAppDispatch} from "../hooks/redux";
import {setAuthState} from "../redux/slices/authSlice";


interface IAccountCardProps {
    user: IUser
}

export const AccountCard = ({user}: IAccountCardProps) => {
    const [deleteCommand] = useDeleteUserMutation()
    const dispatch = useAppDispatch()
    const navigate = useNavigate()

    const handleDelete = async () => {
        try {
            await deleteCommand(user.id)
            handleExit()
            navigate("/")
        } catch {
        }
    }

    const handleExit = () => {
        dispatch(setAuthState(null))
    }

    return (
        <Card className="text-center">
            <Card.Header>
                <Card.Title>
                    Об аккаунте
                </Card.Title>
            </Card.Header>
            <Card.Body>
                <VscAccount
                    size="3em"
                    style={{
                        marginBottom: "20px"
                    }}
                />
                <Card.Text>
                    <b>Имя:</b> {user.login}
                </Card.Text>
            </Card.Body>
            <Card.Footer>
                <Row style={{ padding: "10px" }}>
                    <Button onClick={handleExit}>
                        Выйти
                    </Button>
                </Row>
                <Row style={{ padding: "10px" }}>
                    <Button onClick={handleDelete} variant="outline-danger">
                        Удалить аккаунт
                    </Button>
                </Row>
            </Card.Footer>
        </Card>
    )
}