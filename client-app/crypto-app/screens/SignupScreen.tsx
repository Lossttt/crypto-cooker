import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity, Alert, StyleSheet, ScrollView } from "react-native";
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
    const [email, setEmail] = useState<string>('');
    const [phoneNumber, setPhoneNumber] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [passwordConfirmation, setPasswordConfirmation] = useState<string>('');
    const [isEmailValid, setIsEmailValid] = useState<boolean>(false);
    const [isPhoneValid, setIsPhoneValid] = useState<boolean>(false);
    const [arePasswordsMatching, setArePasswordsMatching] = useState<boolean>(false);
    const [isPasswordValid, setIsPasswordValid] = useState<boolean>(false);
    const [loading, setLoading] = useState<boolean>(false);

    const animationUrl = require("../assets/animations/airplane-loader.json");

    const handleGoBack = () => {
        navigation.goBack();
    };

    const handleEmailChange = (email: string): void => {
        setIsEmailValid(validateEmail(email));
        setEmail(email);
    };

    const handlePhoneChange = (phone: string): void => {
        setIsPhoneValid(validatePhone(phone));
        setPhoneNumber(phone);
    };

    const handlePasswordChange = (password: string, confirmPassword: string): void => {
        setIsPasswordValid(validatePassword(password));
        setArePasswordsMatching(validatePasswordMatch(password, confirmPassword));
        setPassword(password);
        setPasswordConfirmation(confirmPassword);
    };

    const handleSignUp = async () => {
        if (!email || !phoneNumber || !password || !passwordConfirmation) {
            Alert.alert("Validation Error", "Please fill in all the required fields.");
            return;
        }

        if (!isEmailValid || !isPhoneValid || !isPasswordValid || !arePasswordsMatching) {
            Alert.alert("Validation Error", "Please ensure all fields are filled correctly.");
            return;
        }

        setLoading(true);

        const signUpRequest = {
            email,
            phoneNumber,
            password,
            passwordConfirmation,
        };

        try {
            const response = await signUp(signUpRequest);
    
            setTimeout(() => {
                if (response.email && response.userId) {
                    Alert.alert("Sign up Successful", "Welcome to Crypto Cooker!");
                    handleGoBack();
                } else {
                    Alert.alert("Oops!.. Something went wrong", "An error occurred while signing up. Please try again.");
                }
                setLoading(false);
            }, 3000);
        } catch (error) {
            console.log(error);
            setTimeout(() => {
                Alert.alert("Oops!.. Something went wrong", "An error occurred while signing up. Please try again later.");
                setLoading(false);
            }, 3000);
        }
    };

    if (loading) {
        return <LoadingScreen title="Signing Up" message="Please wait while we create your account." animationUrl={animationUrl} />;
    }

    return (
        <ScrollView contentContainerStyle={styles.container}>
            <TouchableOpacity style={styles.backButtonWrapper}>
                <Ionicons name="arrow-back-outline" color={theme.primary} size={25} onPress={() => handleGoBack()} />
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
        left: -150,
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
