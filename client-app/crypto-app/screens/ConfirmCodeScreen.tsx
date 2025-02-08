import React, { useState, useEffect } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from 'react-native';
import { useNavigation, NavigationProp } from '@react-navigation/native';
import { RootStackParamList } from '../types/navigationTypes';
import { theme } from '../shared/styles/theme';
import { fonts } from '../shared/styles/font';
import LottieView from 'lottie-react-native';
import { authService } from '../https/authService';
import { useAuthContext } from '../contexts/authContext';

const ConfirmCodeScreen = () => {
    const { email, setCode } = useAuthContext();
    const [codeInput, setCodeInput] = useState('');
    const [isReady, setIsReady] = useState(false);
    const [animationError, setAnimationError] = useState(false);
    const navigation = useNavigation<NavigationProp<RootStackParamList>>();

    useEffect(() => {
        if (email) {
            setIsReady(true);
        }
        try {
            require('../assets/animations/confirmation-code.json');
        } catch (error) {
            setAnimationError(true);
            console.error("Lottie animation error:", error);
        }
    }, [email]);

    const handleVerifyCode = async () => {
        const resultMessage = await authService.verifyCode(email, codeInput);
        if (resultMessage) {
            setCode(codeInput);
            navigation.navigate('ChangePassword');
        } else {
            Alert.alert('Invalid code', 'The code entered is invalid or expired.');
        }
    };

    if (!isReady) {
        return <View style={styles.loadingContainer}><Text>Loading...</Text></View>;
    }

    return (
        <View style={styles.container}>
            {!animationError ? (
                <LottieView
                    source={require('../assets/animations/confirmation-code.json')}
                    autoPlay
                    loop
                    style={styles.lottie}
                />
            ) : (
                <Text style={styles.errorText}>Animation failed to load. Please try again later.</Text>
            )}

            <Text style={styles.heading}>Enter confirmation code</Text>
            <Text style={styles.subHeadingText}>
                We've sent a confirmation code to {"\n"} <Text style={{ fontFamily: fonts.SemiBold, color: theme.primaryColor }}>{email}</Text>.
                Please enter it below to proceed.
            </Text>

            <TextInput
                style={styles.input}
                placeholder="Enter code"
                keyboardType="numeric"
                value={codeInput}
                onChangeText={setCodeInput}
            />

            <TouchableOpacity style={styles.button} onPress={handleVerifyCode}>
                <Text style={styles.buttonText}>Verify Code</Text>
            </TouchableOpacity>
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
    lottie: {
        width: 150,
        height: 150,
        alignSelf: 'center',
        marginBottom: 30,
    },
    errorText: {
        color: 'red',
        fontSize: 16,
        textAlign: 'center',
        marginTop: 20,
    },
    heading: {
        fontSize: 24,
        fontFamily: fonts.SemiBold,
        textAlign: 'center',
        color: theme.primaryColor,
        marginBottom: 20,
    },
    subHeadingText: {
        fontSize: 14,
        color: theme.secondaryColor,
        fontFamily: fonts.Regular,
        textAlign: "center",
        marginBottom: 30,
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
        marginTop: 0,
        alignItems: 'center',
    },
    buttonText: {
        color: theme.white,
        fontSize: 18,
        fontFamily: fonts.SemiBold,
    },
    loadingContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
});

export default ConfirmCodeScreen;