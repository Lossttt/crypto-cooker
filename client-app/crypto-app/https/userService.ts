import { SignInRequest, SignInResponse } from '../types/user/signInTypes';
import { SignUpRequest, SignUpResponse } from '../types/user/SignUpTypes';
import { apiService } from './apiService';


// SignUp Method
export const signUp = async (data: SignUpRequest): Promise<SignUpResponse> => {
  const response = await apiService.post<SignUpResponse>('/signup', data);
  return response.data;
};

export const signIn = async (data: SignInRequest): Promise<SignInResponse> => {
  const response = await apiService.post<SignInResponse>('/signin', data);
  return response.data;
};
