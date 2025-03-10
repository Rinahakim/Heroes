import { HeroModel } from "./heroModel";

export interface User {
    userName: string;
    password: string;
    data: HeroModel[];
}
