import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, Alert, StyleSheet } from 'react-native';
import { useAuth } from '../hooks/useAuth';
import { useNavigation } from '@react-navigation/native';
import { theme } from '../shared/styles/theme';
import { fonts } from '../shared/styles/font';
import Ionicons from "react-native-vector-icons/Ionicons";
import { validatePassword, validatePasswordMatch } from "../utils/signupValidation";

import { RouteProp, useRoute } from '@react-navigation/native';

import { StackNavigationProp } from '@react-navigation/stack';
import { RootStackParamList } from '../types/navigationTypes';

type ChangePasswordScreenRouteProp = RouteProp<{ params: { userEmail: string; token: string } }, 'params'>;

const ChangePasswordScreen = ({ route }: { route: ChangePasswordScreenRouteProp }) => {
    const navigation = useNavigation<StackNavigationProp<RootStackParamList>>();
    route = useRoute();
    const email = route.params?.userEmail;
    const token = route.params?.token;

    const [newPassword, setNewPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const { loading, message, changePassword } = useAuth();

    const handleChangePassword = async () => {
        if (!validatePassword(newPassword)) {
            Alert.alert('Invalid Password', 'Password must be at least 8 characters long, contain an uppercase letter, a number, and a special character.');
            return;
        }

        if (!validatePasswordMatch(newPassword, confirmPassword)) {
            Alert.alert('Error', 'Passwords do not match');
            return;
        }

        const resultMessage = await changePassword(email, newPassword, token);

        if (resultMessage) {
            Alert.alert('Success', 'Password changed successfully');
            navigation.navigate('Login');
        } else {
            Alert.alert('Error', 'Failed to reset the password');
        }
    };

    return (
        <View style={styles.container}>
            <View style={styles.iconWrapper}>
                <Ionicons name="shield-checkmark-outline" size={50} color={theme.primaryColor} />
            </View>
            <Text style={styles.heading}>Enter a new password</Text>

            <TextInput
                style={styles.input}
                placeholder="New password"
                secureTextEntry
                value={newPassword}
                onChangeText={setNewPassword}
            />
            <TextInput
                style={styles.input}
                placeholder="Confirm new password"
                secureTextEntry
                value={confirmPassword}
                onChangeText={setConfirmPassword}
            />

            <TouchableOpacity style={styles.button} onPress={handleChangePassword}>
                <Text style={styles.buttonText}>Change Password</Text>
            </TouchableOpacity>

            {loading && <Text style={styles.loadingText}>Loading...</Text>}
            {message && <Text style={styles.messageText}>{message}</Text>}
        </View>
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
    input: {
        height: 50,
        paddingHorizontal: 20,
        borderWidth: 1,
        borderColor: theme.secondaryColor,
        borderRadius: 30,
        marginBottom: 20,
        fontSize: 16,
        fontFamily: fonts.Light,
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
    loadingText: {
        fontSize: 14,
        color: theme.primaryColor,
        fontFamily: fonts.Regular,
        textAlign: 'center',
        marginTop: 20,
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