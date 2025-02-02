export interface SignUpRequest {
    email: string;
    password: string;
    confirmPassword: string;
    username: string;
    // TO DO: Add more fields here
}

export interface SignUpResponse {
    success: boolean;
    message: string;
    user?: {
        id: string;
        username: string;
        email: string;
        createdAt: string;
        updatedAt: string;
        // TO DO: Add more fields here
    };
}
