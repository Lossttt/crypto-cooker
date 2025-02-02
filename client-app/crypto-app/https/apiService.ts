import axios, { AxiosRequestConfig, AxiosResponse, AxiosError } from 'axios';
import { API_BASE_URL } from '../config/apiConfig';
import { SignInRequest } from '../types/user/signInTypes';

interface HttpHeaders {
    [key: string]: string;
}

export class ApiService {
    public url: string;
    public loader: boolean;
    public headers: HttpHeaders;
    private readonly initialRetryCount = 1;
    private retryCount = 1;
    private doCatchError = true;

    constructor() {
        this.url = API_BASE_URL;
        this.loader = false;
        this.headers = {
            'Content-Type': 'application/json',
        };
    }

    setUrl(url: string) {
        this.url = url;
    }

    setHeaders(headers: HttpHeaders) {
        this.headers = { ...this.headers, ...headers };
    }

    post<T>(url: string, data: any): Promise<{ data: T }> {
        throw new Error('Method not implemented.');
    }
}

export const apiService = new ApiService();
