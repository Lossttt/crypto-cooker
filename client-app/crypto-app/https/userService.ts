import { SignUpRequest, SignUpResponse } from '../types/user/SignUpTypes';
import { apiEnvironment } from '../config/apiConfig';
import axios from 'axios';

export const signUp = async (data: SignUpRequest): Promise<SignUpResponse> => {
  const response = await axios.post<SignUpResponse>(`${apiEnvironment.service_app_public_url}/users/register`, data);
  return response.data;
};
