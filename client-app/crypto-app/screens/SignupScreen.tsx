import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity, StyleSheet, ScrollView } from "react-native";
import { theme } from "../shared/styles/theme";
import { fonts } from "../shared/styles/font";
import { signUp } from "../https/userService";
import LoadingScreen from "./emptyStates/LoadingScreen";
import Ionicons from "react-native-vector-icons/Ionicons";
import SimpleLineIcons from "react-native-vector-icons/SimpleLineIcons";
import { NavigationProp, useNavigation } from "@react-navigation/native";
import { RootStackParamList } from "../types/navigationTypes";
import { validateEmail, validatePhone, validatePassword, validatePasswordMatch } from "../utils/signupValidation";

const SignUpScreen = () => {
    const navigation = useNavigation<NavigationProp<RootStackParamList>>();
    const [email, setEmail] = useState<string>("");
    const [phoneNumber, setPhoneNumber] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [passwordConfirmation, setPasswordConfirmation] = useState<string>("");
    const [isEmailValid, setIsEmailValid] = useState<boolean>(false);
    const [isPhoneValid, setIsPhoneValid] = useState<boolean>(false);
    const [arePasswordsMatching, setArePasswordsMatching] = useState<boolean>(false);
    const [isPasswordValid, setIsPasswordValid] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(false);
    const [errorMessage, setErrorMessage] = useState<string>("");

    const animationUrl = require("../assets/animations/airplane-loader.json");

    const handleGoBack = () => {
        navigation.goBack();
    };

    const handleEmailChange = (email: string): void => {
        setEmail(email);
        if (email.length > 3) {
            setIsEmailValid(validateEmail(email));
        }
    };

    const handlePhoneChange = (phone: string): void => {
        setPhoneNumber(phone);
        if (phone.length >= 10) {
            setIsPhoneValid(validatePhone(phone));
        }
    };

    const handlePasswordChange = (password: string, confirmPassword: string): void => {
        setPassword(password);
        setPasswordConfirmation(confirmPassword);
        if (password.length >= 8) {
            setIsPasswordValid(validatePassword(password));
        }
        if (password === confirmPassword) {
            setArePasswordsMatching(true);
        } else {
            setArePasswordsMatching(false);
        }
    };

    const handleSignUp = async () => {
        if (!email || !phoneNumber || !password || !passwordConfirmation) {
            return;
        }

        if (!isPasswordValid || !isEmailValid || !isPhoneValid || !arePasswordsMatching) {
            return;
        }

        setLoading(true);
        setErrorMessage("");  // Reset any previous error message

        const signUpRequest = {
            email,
            phoneNumber,
            password,
            passwordConfirmation,
        };

        try {
            const response = await signUp(signUpRequest);

            if (response?.email && response?.userId) {
                navigation.goBack();
            } else {
                setErrorMessage("An error occurred while signing up. Please try again.");
            }
        } catch (error) {
                setErrorMessage("An error occurred while signing up. Please try again later.");
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return <LoadingScreen title="Signing Up" message="Please wait while we create your account." animationUrl={animationUrl} />;
    }

    return (
        <ScrollView contentContainerStyle={styles.container}>
            <TouchableOpacity style={styles.backButtonWrapper} onPress={handleGoBack}>
                <Ionicons name="arrow-back-outline" color={theme.primary} size={25} />
            </TouchableOpacity>
            <Text style={styles.headingText}>Create Your Account</Text>

            <View style={styles.inputContainer}>
                <Ionicons name="mail-outline" size={20} color={theme.primary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="Email"
                    value={email}
                    onChangeText={handleEmailChange}
                    keyboardType="email-address"
                />
                {isEmailValid && <Ionicons name="checkmark-done-outline" size={20} color={theme.buttonBackground} />}
            </View>
            {!isEmailValid && email.length > 3 && (
                <Text style={styles.errorText}>Please enter a valid email address.</Text>
            )}

            <View style={styles.inputContainer}>
                <SimpleLineIcons name="phone" size={20} color={theme.primary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="Phone number"
                    value={phoneNumber}
                    onChangeText={handlePhoneChange}
                    keyboardType="phone-pad"
                />
                {isPhoneValid && <Ionicons name="checkmark-done-outline" size={20} color={theme.buttonBackground} />}
            </View>
            {!isPhoneValid && phoneNumber.length >= 10 && (
                <Text style={styles.errorText}>Please enter a valid phone number.</Text>
            )}

            <View style={styles.inputContainer}>
                <SimpleLineIcons name="lock" size={20} color={theme.primary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="Password"
                    value={password}
                    onChangeText={(value) => handlePasswordChange(value, passwordConfirmation)}
                    secureTextEntry
                />
                {isPasswordValid && <Ionicons name="checkmark-done-outline" size={20} color={theme.buttonBackground} />}
            </View>
            {!isPasswordValid && password.length >= 8 && (
                <Text style={styles.errorText}>
                    Password must be at least 8 characters long, contain uppercase, lowercase, number, and special character.
                </Text>
            )}

            <View style={styles.inputContainer}>
                <SimpleLineIcons name="lock" size={20} color={theme.primary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="Confirm password"
                    value={passwordConfirmation}
                    onChangeText={(value) => handlePasswordChange(password, value)}
                    secureTextEntry
                />
                {arePasswordsMatching && <Ionicons name="checkmark-done-outline" size={20} color={theme.buttonBackground} />}
            </View>
            {!arePasswordsMatching && passwordConfirmation.length > 0 && (
                <Text style={styles.errorText}>Passwords do not match.</Text>
            )}

            {errorMessage && <Text style={styles.errorText}>{errorMessage}</Text>}

            <TouchableOpacity style={styles.signUpButton} onPress={handleSignUp}>
                <Text style={styles.signUpButtonText}>Sign Up</Text>
            </TouchableOpacity>
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flexGrow: 1,
        padding: 20,
        backgroundColor: theme.white,
        alignItems: "center",
    },
    backButtonWrapper: {
        height: 40,
        width: 40,
        backgroundColor: theme.gray,
        borderRadius: 20,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 35,
        marginBottom: 20,
        right: 175,
    },
    headingText: {
        fontSize: 28,
        color: theme.primaryColor,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
        marginBottom: 20,
    },
    inputContainer: {
        marginTop: 10,
        width: "100%",
        borderWidth: 1,
        borderColor: theme.secondaryColor,
        borderRadius: 50,
        paddingHorizontal: 20,
        flexDirection: "row",
        alignItems: "center",
        paddingVertical: 12,
        marginBottom: 15,
    },
    textInput: {
        flex: 1,
        fontFamily: fonts.Light,
        fontSize: 16,
        paddingVertical: 5,
        paddingHorizontal: 10,
    },
    errorText: {
        color: theme.danger,
        fontSize: 14,
        textAlign: "left",
        marginLeft: 20,
        marginBottom: 10,
    },
    signUpButton: {
        backgroundColor: theme.primary,
        paddingVertical: 15,
        borderRadius: 50,
        marginTop: 25,
        width: "100%",
    },
    signUpButtonText: {
        color: theme.white,
        fontSize: 18,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
    },
});

export default SignUpScreen;
