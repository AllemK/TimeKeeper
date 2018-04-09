import { IMaster } from "./employee";

export class IMember {
    team: IMaster;
    role: IMaster;
    employee: IMaster;
    hours: number;
}

export class ITeam {
    id: string;
    name: string;
    image: string;
    members: IMember[];
}