import {
    Image,
    StyleSheet,
    Text,
    TextInput,
    TouchableOpacity,
    View,
    Alert,
} from "react-native";
import React, { useState } from "react";
import { fonts } from "../shared/styles/font";
import { theme } from "../shared/styles/theme";
import Ionicons from "react-native-vector-icons/Ionicons";
import SimpleLineIcons from "react-native-vector-icons/SimpleLineIcons";
import { useNavigation, NavigationProp } from "@react-navigation/native";
import { RootStackParamList } from "../types/navigationTypes";
import { SignInRequest } from "../types/user/signInTypes";
import { authService } from "../https/authService";
import { validateSignInRequest } from "../utils/authValidation";
import LoadingScreen from "./emptyStates/LoadingScreen";

const LoginScreen = () => {
    const navigation = useNavigation<NavigationProp<RootStackParamList>>();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [secureEntry, setSecureEntry] = useState(true);
    const [loading, setLoading] = useState(false);
    const animationUrl = require("../assets/animations/airplane-loader.json");

    const handleGoBack = () => {
        navigation.goBack();
    };

    const handleSignup = () => {
        navigation.navigate('Register');
    };

    const handleSignIn = async () => {
        setLoading(true);

        const request: SignInRequest = { email, password };
        const validationError = validateSignInRequest(request);
        if (validationError) {
            Alert.alert('Oops something went wrong..', validationError);
            setLoading(false);
            return;
        }

        try {
            const response = await authService.signIn(request);
            if (response.accessToken && response.refreshToken) {
                setTimeout(() => {
                    setLoading(false);
                    navigation.navigate("LandingPage");
                }, 2000);
            } else {
                Alert.alert('Oops!.. Login failed', response.message);
                setLoading(false);
            }
        } catch (error) {
            Alert.alert('Oops!.. Login failed', 'An error occurred during login. Please try again.');
            setLoading(false);
        }
    };

    if (loading) {
        return <LoadingScreen title="Logging In" message="You will be redirected shortly" animationUrl={animationUrl} />;
    }
    
    return (
        <View style={styles.container}>
            <TouchableOpacity style={styles.backButtonWrapper} onPress={handleGoBack}>
                <Ionicons name="arrow-back-outline" color={theme.primary} size={25} />
            </TouchableOpacity>

            <View style={styles.textContainer}>
                <Text style={styles.headingText}>Welcome Back!</Text>
                <Text style={styles.subHeadingText}>We missed you!</Text>
            </View>

            <View style={styles.formContainer}>
                <View style={styles.inputContainer}>
                    <Ionicons name="mail-outline" size={20} color={theme.secondary} />
                    <TextInput
                        style={styles.textInput}
                        placeholder="Enter your email"
                        placeholderTextColor={theme.secondary}
                        keyboardType="email-address"
                        value={email}
                        onChangeText={setEmail}
                    />
                </View>

                <View style={styles.inputContainer}>
                    <SimpleLineIcons name="lock" size={20} color={theme.secondary} />
                    <TextInput
                        style={styles.textInput}
                        placeholder="Enter your password"
                        placeholderTextColor={theme.secondary}
                        secureTextEntry={secureEntry}
                        value={password}
                        onChangeText={setPassword}
                    />
                    <TouchableOpacity onPress={() => setSecureEntry(prev => !prev)}>
                        <SimpleLineIcons name="eye" size={20} color={theme.secondary} />
                    </TouchableOpacity>
                </View>

                <TouchableOpacity>
                    <Text style={styles.forgotPasswordText} onPress={() => navigation.navigate("ResetPassword")}>Forgot Password?</Text>
                </TouchableOpacity>

                <TouchableOpacity style={styles.loginButtonWrapper} onPress={handleSignIn}>
                    <Text style={styles.loginText}>Login</Text>
                </TouchableOpacity>

                <Text style={styles.continueText}>or continue with</Text>

                <TouchableOpacity style={styles.googleButtonContainer}>
                    <Image source={require("../assets/google.png")} style={styles.googleImage} />
                    <Text style={styles.googleText}>Google</Text>
                </TouchableOpacity>

                <View style={styles.footerContainer}>
                    <Text style={styles.accountText}>Don’t have an account?</Text>
                    <TouchableOpacity onPress={handleSignup}>
                        <Text style={styles.signupText}>Sign up</Text>
                    </TouchableOpacity>
                </View>
            </View>
        </View>
    );
};

export default LoginScreen;

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: theme.white,
        padding: 20,
    },
    backButtonWrapper: {
        height: 40,
        width: 40,
        backgroundColor: theme.gray,
        borderRadius: 20,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 35,
    },
    textContainer: {
        marginVertical: 20,
    },
    headingText: {
        fontSize: 32,
        color: theme.primaryColor,
        fontFamily: fonts.SemiBold,
    },
    subHeadingText: {
        fontSize: 18,
        color: theme.secondaryColor,
        fontFamily: fonts.Regular,
    },
    formContainer: {
        marginTop: 20,
    },
    inputContainer: {
        borderWidth: 1,
        borderColor: theme.secondaryColor,
        borderRadius: 50,
        paddingHorizontal: 20,
        flexDirection: "row",
        alignItems: "center",
        paddingVertical: 10,
        marginVertical: 10,
    },
    textInput: {
        flex: 1,
        fontFamily: fonts.Light,
        fontSize: 16,
        paddingVertical: 5,
        paddingHorizontal: 10,
    },
    forgotPasswordText: {
        fontSize: 12,
        textAlign: "right",
        color: theme.primaryColor,
        fontFamily: fonts.SemiBold,
        marginVertical: 10,
        marginRight: 5,
    },
    loginButtonWrapper: {
        backgroundColor: theme.buttonBackground,
        borderRadius: 100,
        marginTop: 20,
    },
    loginText: {
        color: theme.white,
        fontSize: 18,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
        paddingVertical: 15,
    },
    continueText: {
        textAlign: "center",
        marginVertical: 20,
        fontSize: 14,
        fontFamily: fonts.Regular,
        color: theme.primaryColor,
    },
    googleButtonContainer: {
        flexDirection: "row",
        borderWidth: 2,
        borderColor: theme.primaryColor,
        borderRadius: 50,
        justifyContent: "center",
        alignItems: "center",
        paddingVertical: 12,
        gap: 15,
    },
    googleImage: {
        height: 20,
        width: 20,
    },
    googleText: {
        fontSize: 16,
        fontFamily: fonts.Medium,
    },
    footerContainer: {
        flexDirection: "row",
        justifyContent: "center",
        alignItems: "center",
        marginVertical: 20,
    },
    accountText: {
        fontSize: 14,
        color: theme.primaryColor,
        fontFamily: fonts.Regular,
    },
    signupText: {
        fontSize: 14,
        color: theme.primary,
        fontFamily: fonts.Bold,
        marginLeft: 5,
    },
});