import {IAnswer} from "./IAnswer";

export interface IQuestion {
    id: number
    text: string
    testId: number
    answers: IAnswer[]
}