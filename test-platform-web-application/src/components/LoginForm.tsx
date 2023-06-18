import {Controller, SubmitHandler, useForm, useFormState} from "react-hook-form";
import {Button, Container, TextField, Typography} from "@mui/material";
import {ValidationMessages} from "../common/ValidationMessages";
import {useLoginMutation} from "../services/authApi";
import {ILoginCommand} from "../models/ILoginCommand";
import { useNavigate } from "react-router-dom";
import {useDialog} from "../hooks/useDialog";
import {ErrorDialog} from "./ErrorDialog";

export const LoginForm = () => {
    const {control, handleSubmit} = useForm<ILoginCommand>({ mode: "onChange" })
    const {errors} = useFormState({ control })
    const [login] = useLoginMutation()
    const navigate = useNavigate()
    const errorDialog = useDialog()

    const onSubmit: SubmitHandler<ILoginCommand> = async (command) => {
        const result = await login(command)

        if ("data" in result && result.data) {
            navigate("/account")
        } else {
            errorDialog.handleOpen()
        }
    }

    return (
        <>
            <ErrorDialog
                open={errorDialog.open}
                onClose={errorDialog.handleClose}
                message={"Неверный логин или пароль"}
            />
            <Container maxWidth="sm">
                <form onSubmit={handleSubmit(onSubmit)}>
                    <Typography
                        variant="h4"
                        component="h2"
                        align="center"
                        gutterBottom
                        style={{
                            fontSize: "32px",
                            fontWeight: 700,
                            color: "black"
                        }}
                    >
                        Вход
                    </Typography>
                    <Controller
                        name="login"
                        control={control}
                        rules={{ required: ValidationMessages.Required }}
                        render={({ field }) => (
                            <TextField
                                label="Логин"
                                variant="outlined"
                                margin="normal"
                                fullWidth
                                value={field.value}
                                onChange={field.onChange}
                                error={!!errors.login?.message}
                                helperText={errors.login?.message}
                            />
                        )}
                    />
                    <Controller
                        name="password"
                        control={control}
                        rules={{ required: ValidationMessages.Required }}
                        render={({ field }) => (
                            <TextField
                                label="Пароль"
                                variant="outlined"
                                margin="normal"
                                type="password"
                                fullWidth
                                value={field.value}
                                onChange={field.onChange}
                                error={!!errors.password?.message}
                                helperText={errors.password?.message}
                            />
                        )}
                    />
                    <Button
                        style={{
                            marginTop: "20px"
                        }}
                        size="large"
                        type="submit"
                        variant="contained"
                        color="primary"
                        fullWidth
                    >
                        Войти
                    </Button>
                    <Button
                        href="registration"
                        style={{
                            marginTop: "20px"
                        }}
                        size="large"
                        variant="outlined"
                        color="primary"
                        fullWidth
                    >
                        Регистрация
                    </Button>
                    <Button
                        href="/"
                        style={{
                            marginTop: "20px"
                        }}
                        size="large"
                        variant="outlined"
                        color="primary"
                        fullWidth
                    >
                        Главная
                    </Button>
                </form>
            </Container>
        </>
    )
}