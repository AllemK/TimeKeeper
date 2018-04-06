export class ICustomer {
    id: number;
    name: string;
    monogram: string;
    contact: string;
    email: string;
    phone: string;
    address: IAddress;
    status: number;
}

export class IAddress {
    road: string;
    zipCode: string;
    city: string;
}