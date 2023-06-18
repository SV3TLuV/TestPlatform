import {ValidationMessages} from "../../common/ValidationMessages";

export const TestNameValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 10
        const maxLength = 100

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}

export const TestDescriptionValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 30
        const maxLength = 500

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}

export const QuestionTextValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 3
        const maxLength = 150

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}

export const AnswerTextValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 1
        const maxLength = 200

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}