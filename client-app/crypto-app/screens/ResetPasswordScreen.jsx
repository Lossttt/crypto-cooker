import React, { useState } from "react";
import { View, Text, TextInput, TouchableOpacity, Alert, StyleSheet } from "react-native";
import { useNavigation } from "@react-navigation/native";
import { authService } from "../https/authService";
import { theme } from "../shared/styles/theme";
import { fonts } from "../shared/styles/font";
import LoadingScreen from "./emptyStates/LoadingScreen";
import Ionicons from "react-native-vector-icons/Ionicons";
import { useAuthContext } from "../contexts/authContext";

const ResetPasswordScreen = () => {
    const navigation = useNavigation();
    const { setEmail } = useAuthContext();
    const [emailInput, setEmailInput] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');

    const animationUrl = require('../assets/animations/airplane-loader.json');
    const handleGoBack = () => {
        navigation.goBack();
    };

    const handleResetPassword = async () => {
        if (!emailInput) {
            Alert.alert('Oops!..', 'Please enter your email');
            return;
        }

        setLoading(true);

        try {
            const response = await authService.resetPassword(emailInput);

            if (response.message) {
                setEmail(emailInput);
                setMessage('A reset email has been sent to your email address with further instructions. Please wait for us to redirect you to the next screen.');

                setTimeout(() => {
                    navigation.navigate('ConfirmCode');
                }, 6000);

            } else {
                Alert.alert('Oops!.. Something went wrong', response.message);
            }
        } catch (error) {
            Alert.alert('Oops!.. Something went wrong', 'An error occurred while sending the reset email. Please try again.');
        } finally {
            setLoading(false);
        }
    };

    if (loading) {
        return <LoadingScreen title="Processing" message="Please wait while we send the reset email." animationUrl={animationUrl}/>;
    }

    return (
        <View style={styles.container}>
            <View style={styles.iconWrapper}>
                <Ionicons name="finger-print" size={50} color={theme.primaryColor} />
            </View>
            <TouchableOpacity style={styles.backButtonWrapper} onPress={handleGoBack}>
                <Ionicons name="arrow-back-outline" color={theme.primary} size={25} />
            </TouchableOpacity>
            <Text style={styles.headingText}>Reset your password</Text>
            <Text style={styles.subHeadingText}>Enter your email below to receive password reset instructions.</Text>

            <View style={styles.inputContainer}>
                <TextInput
                    style={styles.textInput}
                    placeholder="Enter your email"
                    value={emailInput}
                    onChangeText={setEmailInput}
                    keyboardType="email-address"
                />
            </View>

            <TouchableOpacity style={styles.resetButton} onPress={handleResetPassword}>
                <Text style={styles.resetButtonText}>Send reset link</Text>
            </TouchableOpacity>

            {message && <Text style={styles.successMessage}>{message}</Text>}
            {loading && <LoadingScreen title="Processing" message="Please wait while we send the reset email." />}
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: "center",
        padding: 20,
        backgroundColor: theme.white,
    },
    backButtonWrapper: {
        height: 40,
        width: 40,
        backgroundColor: theme.gray,
        borderRadius: 20,
        justifyContent: "center",
        alignItems: "center",
        marginTop: 35,
        position: "absolute",
        top: 20,
        left: 20,
    },
    iconWrapper: {
        borderWidth: 1,
        borderColor: theme.gray,
        borderRadius: 15,
        padding: 10,
        width: 70,
        height: 70,
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 20,
        alignSelf: 'center',
    },
    headingText: {
        fontSize: 28,
        color: theme.primaryColor,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
        marginBottom: 10,
    },
    subHeadingText: {
        fontSize: 16,
        color: theme.secondaryColor,
        fontFamily: fonts.Regular,
        textAlign: "center",
        marginBottom: 30,
    },
    inputContainer: {
        borderWidth: 1,
        borderColor: theme.secondaryColor,
        borderRadius: 50,
        paddingHorizontal: 20,
        flexDirection: "row",
        alignItems: "center",
        paddingVertical: 10,
        marginBottom: 20,
    },
    textInput: {
        flex: 1,
        fontFamily: fonts.Light,
        fontSize: 16,
        paddingVertical: 5,
        paddingHorizontal: 10,
    },
    resetButton: {
        backgroundColor: theme.primary,
        paddingVertical: 15,
        borderRadius: 50,
        marginTop: 10,
    },
    resetButtonText: {
        color: theme.white,
        fontSize: 18,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
    },
    successMessage: {
        fontSize: 14,
        color: theme.primaryColor,
        fontFamily: fonts.Regular,
        textAlign: "center",
        marginTop: 20,
    },
});

export default ResetPasswordScreen;