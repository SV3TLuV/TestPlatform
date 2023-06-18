import {ValidationMessages} from "../../common/ValidationMessages";

const availableSymbols = [
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
    'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
    's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A',
    'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
    'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
    'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1',
    '2', '3', '4', '5', '6', '7', '8', '9'
]

export const LoginValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 3
        const maxLength = 50

        for (let i = 0; i < value.length; i++) {
            if (!availableSymbols.includes(value[i])) {
                return "Разрешенные символы: a-Z, 0-9"
            }
        }

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}

export const PasswordValidation = {
    required: ValidationMessages.Required,
    validate: (value: string) => {
        const minLength = 8
        const maxLength = 200

        const symbols = availableSymbols
            .concat('!', '@', '%', '&', '*', '#')

        Array.from(value).forEach(s => {
            if (!symbols.includes(s)) {
                return "Разрешенные символы: a-Z, 0-9, !, @, %, &, *, #"
            }
        })

        if (value.length < minLength)
            return `Минимальная длинна ${minLength} символов`

        if (value.length > maxLength)
            return `Максимальная длинна ${maxLength} символов`

        return true
    }
}