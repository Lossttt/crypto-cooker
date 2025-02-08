import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { SignInRequest, SignInResponse } from '../types/user/signInTypes';
import { apiEnvironment } from '../config/apiConfig';

const AUTH_TOKEN_KEY = 'auth_token';
const REFRESH_TOKEN_KEY = 'refresh_token';
const API_URL = apiEnvironment.service_app_public_url;

const handleApiError = (error: any) => {
    const errorMessage = error.response?.data?.message || 'An unexpected error occurred.';
    throw new Error(errorMessage);
};

export const authService = {
    async signIn(data: SignInRequest): Promise<SignInResponse> {
        const response = await axios.post<SignInResponse>(`${API_URL}/users/authenticate`, data);
        if (response.data.accessToken && response.data.refreshToken) {
            await this.saveTokens(response.data.accessToken.token, response.data.refreshToken);
        } else {
            throw new Error('Authentication tokens are missing');
        }
        return response.data;
    },

    async saveTokens(token: string, refreshToken: string) {
        await AsyncStorage.setItem(AUTH_TOKEN_KEY, token);
        await AsyncStorage.setItem(REFRESH_TOKEN_KEY, refreshToken);
    },

    async getAuthToken(): Promise<string | null> {
        return await AsyncStorage.getItem(AUTH_TOKEN_KEY);
    },

    async getRefreshToken(): Promise<string | null> {
        return await AsyncStorage.getItem(REFRESH_TOKEN_KEY);
    },

    async clearTokens() {
        await AsyncStorage.removeItem(AUTH_TOKEN_KEY);
        await AsyncStorage.removeItem(REFRESH_TOKEN_KEY);
    },

    async resetPassword(email: string) {
        try {
            const response = await axios.post(`${API_URL}/users/reset-password/request`, { email });
            return response.data;
        } catch (error) {
            handleApiError(error);
        }
    },

    async verifyCode(email: string, code: string) {
        try {
            const response = await axios.post(`${API_URL}/users/reset-password/verify`, { email, token: code });
            return response.data;
        } catch (error) {
            handleApiError(error);
        }
    },

    async changePassword(email: string, newPassword: string, token: string) {
        try {
            const response = await axios.post(`${API_URL}/users/reset-password`, { email, newPassword, token });
            return response.data;
        } catch (error) {
            handleApiError(error);
        }
    },

    async isAccountVerified(email: string): Promise<boolean> {
        try {
            const response = await axios.post(`${API_URL}/users/verify/check`, { email });
            return response.data.isVerified;
        } catch (error) {
            handleApiError(error);
            return false;
        }
    }
};