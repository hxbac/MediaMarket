export interface UserResponse {
    id: string;
    name: string;
    userName: string;
    email: string;
    address: string | null;
    avatar: string | null;
    description: string | null;
    phoneNumber: string | null;
    birthday: string | null;
}

export interface Seller {
    id: string;
    name: string;
    avatar: string | null;
}
