import {Controller, SubmitHandler, useForm, useFormState} from "react-hook-form";
import {Button, Container, TextField, Typography} from "@mui/material";
import React from "react";
import {ValidationMessages} from "../../common/ValidationMessages";
import {useNavigate} from "react-router-dom";
import {useCreateUserMutation} from "../../services/userApi";
import {IRegisterCommand} from "../../models/IRegisterCommand";
import {LoginValidation, PasswordValidation} from "./registrationFormValidation";
import {useDialog} from "../../hooks/useDialog";
import {ErrorDialog} from "../ErrorDialog";


export const RegistrationForm = () => {
    const {control, handleSubmit, setError} = useForm<IRegisterCommand>({ mode: "onChange" })
    const {errors} = useFormState({ control })
    const [create] = useCreateUserMutation()
    const navigate = useNavigate()
    const errorDialog = useDialog()

    const onSubmit: SubmitHandler<IRegisterCommand> = async (command) => {
        if (command.password !== command.confirm) {
            const error = { type: "manual", message: "Пароли не совпадают" }
            setError("password", error)
            setError("confirm", error)
        } else {
            const result = await create(command)

            if ("data" in result && result.data) {
                navigate("/login")
            } else {
                errorDialog.handleOpen()
            }
        }
    }

    return (
        <>
            <ErrorDialog
                open={errorDialog.open}
                onClose={errorDialog.handleClose}
                message={"Пользователь с таким именем уже существует"}
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
                        Регистрация
                    </Typography>
                    <Controller
                        name="login"
                        control={control}
                        rules={LoginValidation}
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
                        rules={PasswordValidation}
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
                    <Controller
                        name="confirm"
                        control={control}
                        rules={{ required: ValidationMessages.Required }}
                        render={({ field }) => (
                            <TextField
                                label="Подтверждение"
                                variant="outlined"
                                margin="normal"
                                type="password"
                                fullWidth
                                value={field.value}
                                onChange={field.onChange}
                                error={!!errors.confirm?.message}
                                helperText={errors.confirm?.message}
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
                        Зарегистрироваться
                    </Button>
                    <Button
                        href="login"
                        style={{
                            marginTop: "20px"
                        }}
                        size="large"
                        variant="outlined"
                        color="primary"
                        fullWidth
                    >
                        Вход
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