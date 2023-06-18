import {IUser} from "./IUser";
import {ITokenPair} from "./ITokenPair";

export interface IAuthResponse {
    tokens: ITokenPair
    user: IUser
}
