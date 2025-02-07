export type RootStackParamList = {
    Home: undefined;
    Login: undefined;
    Register: undefined;
    LandingPage: undefined;
    ResetPassword: undefined;
    ConfirmCode: undefined;
    ChangePassword: { email: string; token: string };
};