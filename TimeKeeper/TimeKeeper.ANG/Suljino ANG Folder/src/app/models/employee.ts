export interface IEmployee {
    id: number;
    image: string;
    firstName: string;
    lastName: string;
    fullName: string;
    email: string;
    phone: string;
    birthDate: Date;
    beginDate: Date;
    endDate: Date;
    status: number;
    position: IMaster;
    salary: number;
}

export interface IMaster {
    id: string;
    name: string;
}

export interface IPagination {
    currentPage: number;
    pageCount: number;
    itemCount: number;
    pageSize: number;
}
