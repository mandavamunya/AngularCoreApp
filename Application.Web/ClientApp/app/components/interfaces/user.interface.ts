import { Role } from './roles.interface';

export interface User 
{
    firstName: string;
    lastName: string;
    username: string;
    isEmailConfirmed: boolean;
    email: string;
    phoneNumber: string;
    statusMessage: string;
    role: string;
}

export interface UserViewModel {
    firstName: string;
    lastName: string;
    username: string;
    emailConfirmed: boolean;
    email: string;
    phoneNumber: string;
}