import { SignInRequest } from '../types/user/signInTypes';

export const validateSignInRequest = (request: SignInRequest): string | null => {
    if (!request.email || !request.password) {
        return 'Please fill in all the fields';
    }
    if (!request.email.includes('@')) {
        return 'Please enter a valid email address';
    }
    return null;
};