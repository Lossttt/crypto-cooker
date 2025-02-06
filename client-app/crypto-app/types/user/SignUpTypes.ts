export interface SignUpRequest {
    email: string;
    password: string;
    passwordConfirmation: string;
    phoneNumber: string;
}

export interface SignUpResponse {
    userId: string;
    email: string;
}
