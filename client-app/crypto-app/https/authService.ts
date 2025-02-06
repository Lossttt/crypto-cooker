import axios from 'axios';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { SignInRequest, SignInResponse } from '../types/user/signInTypes';
import { apiEnvironment } from '../config/apiConfig';
const AUTH_TOKEN_KEY = 'auth_token';
const REFRESH_TOKEN_KEY = 'refresh_token';

export const authService = {
    async signIn(data: SignInRequest): Promise<SignInResponse> {
        const response = await axios.post<SignInResponse>(`${apiEnvironment.service_app_public_url}/users/authenticate`, data);
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
    }
};