import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, ScrollView } from 'react-native';
import { useAuthContext } from '../contexts/authContext';
import { NavigationProp, useNavigation } from '@react-navigation/native';
import { theme } from '../shared/styles/theme';
import { fonts } from '../shared/styles/font';
import Ionicons from "react-native-vector-icons/Ionicons";
import { validatePassword, validatePasswordMatch } from "../utils/signupValidation";
import { RootStackParamList } from '../types/navigationTypes';
import SimpleLineIcons from 'react-native-vector-icons/SimpleLineIcons';

const ChangePasswordScreen = () => {
    const navigation = useNavigation<NavigationProp<RootStackParamList>>();
    const { email, code } = useAuthContext();

    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [isPasswordVisible, setIsPasswordVisible] = useState(false);
    const [isConfirmPasswordVisible, setIsConfirmPasswordVisible] = useState(false);
    const [passwordError, setPasswordError] = useState('');
    const [passwordMatchError, setPasswordMatchError] = useState('');
    const [loading, setLoading] = useState(false);
    const [message, setMessage] = useState('');

    const handlePasswordChange = (password: string) => {
        setNewPassword(password);
        if (!validatePassword(password)) {
            setPasswordError("Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character.");
        } else {
            setPasswordError('');
        }
    };

    const handleConfirmPasswordChange = (confirmPassword: string) => {
        setConfirmPassword(confirmPassword);
        if (!validatePasswordMatch(newPassword, confirmPassword)) {
            setPasswordMatchError("Passwords do not match.");
        } else {
            setPasswordMatchError('');
        }
    };

    const handleChangePassword = async () => {
        if (!newPassword || !confirmPassword) {
            setMessage("Please fill in both fields.");
            return;
        }

        if (passwordError || passwordMatchError) {
            return;
        }

        setLoading(true);
        setMessage('');

        // Here you would normally call your service to change the password
        const resultMessage = 'Password changed successfully'; // For example purpose

        if (resultMessage) {
            setMessage(resultMessage);
            navigation.navigate('Login');
        } else {
            setMessage("Failed to reset the password. Please try again later.");
        }

        setLoading(false);
    };

    return (
        <ScrollView contentContainerStyle={styles.container}>
            <View style={styles.iconWrapper}>
                <Ionicons name="shield-checkmark-outline" size={50} color={theme.primaryColor} />
            </View>
            <Text style={styles.heading}>Enter a new password</Text>

            {/* New Password Field */}
            <View style={styles.inputContainer}>
                <SimpleLineIcons name="lock" size={20} color={theme.secondary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="New password"
                    value={newPassword}
                    onChangeText={handlePasswordChange}
                    secureTextEntry={!isPasswordVisible}
                />
                <TouchableOpacity onPress={() => setIsPasswordVisible(!isPasswordVisible)}>
                    <SimpleLineIcons name="eye" size={20} color={theme.secondary} />
                </TouchableOpacity>
            </View>
            {passwordError ? <Text style={styles.errorText}>{passwordError}</Text> : null}

            {/* Confirm Password Field */}
            <View style={styles.inputContainer}>
                <SimpleLineIcons name="lock" size={20} color={theme.secondary} />
                <TextInput
                    style={styles.textInput}
                    placeholder="Confirm new password"
                    value={confirmPassword}
                    onChangeText={handleConfirmPasswordChange}
                    secureTextEntry={!isConfirmPasswordVisible}
                />
                <TouchableOpacity onPress={() => setIsConfirmPasswordVisible(!isConfirmPasswordVisible)}>
                    <SimpleLineIcons name="eye" size={20} color={theme.secondary} />
                </TouchableOpacity>
            </View>
            {passwordMatchError ? <Text style={styles.errorText}>{passwordMatchError}</Text> : null}

            <TouchableOpacity style={styles.button} onPress={handleChangePassword} disabled={loading}>
                <Text style={styles.buttonText}>{loading ? "Changing..." : "Change Password"}</Text>
            </TouchableOpacity>

            {message && <Text style={styles.messageText}>{message}</Text>}
        </ScrollView>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        padding: 20,
        backgroundColor: theme.white,
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
    heading: {
        fontSize: 24,
        textAlign: 'center',
        marginBottom: 20,
        fontFamily: fonts.SemiBold,
        color: theme.primaryColor,
    },
    inputContainer: {
        height: 50,
        marginBottom: 20,
        flexDirection: 'row',
        alignItems: 'center',
        borderWidth: 1,
        borderColor: theme.secondaryColor,
        borderRadius: 30,
        paddingHorizontal: 20,
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
        textAlign: 'left',
        marginLeft: 20,
        marginBottom: 10,
    },
    button: {
        backgroundColor: theme.primary,
        paddingVertical: 15,
        borderRadius: 30,
        marginTop: 10,
        alignItems: 'center',
    },
    buttonText: {
        color: theme.white,
        fontSize: 18,
        fontFamily: fonts.SemiBold,
    },
    messageText: {
        fontSize: 14,
        color: theme.primaryColor,
        fontFamily: fonts.Regular,
        textAlign: 'center',
        marginTop: 20,
    },
});

export default ChangePasswordScreen;
