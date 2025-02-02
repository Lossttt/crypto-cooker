export interface SignInRequest {
    email: string;
    password: string;
}

export interface SignInResponse {
    success: boolean;
    message: string;
    token?: string; 
    user?: {
        id: string;
        username: string;
        email: string;
        createdAt: string;
        updatedAt: string;
    };
}