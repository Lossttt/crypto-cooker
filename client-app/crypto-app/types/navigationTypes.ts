export type RootStackParamList = {
    Home: undefined;
    Login: undefined;
    Register: undefined;
    LandingPage: undefined;
    ResetPassword: undefined;
    ConfirmCode: { email: string };
    ChangePassword: { email: string; code: string };
};