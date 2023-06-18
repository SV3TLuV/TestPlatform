import {IQuestion} from "./IQuestion";

export interface ITest {
    id: number
    name: string
    description: string
    userId: number
    questions: IQuestion[]
}
