export interface SignInRequest {
    email: string;
    password: string;
}

export interface SignInResponse {
    success: boolean;
    message: string;
    accessToken?: {
        token: string;
        expiredIn: number;
    };
    refreshToken?: string;
    lastLogin?: string;
    firstName?: string;
}