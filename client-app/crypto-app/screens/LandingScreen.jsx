import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, StyleSheet, ImageBackground, Animated } from 'react-native';
import { theme } from '../shared/styles/theme';
import Header from '../components/Header';
import { useNavigation } from '@react-navigation/native';
import { fonts } from '../shared/styles/font';

const LandingScreen = () => {
    const [fadeAnim] = useState(new Animated.Value(0));
    const navigation = useNavigation();

    useEffect(() => {
        Animated.timing(fadeAnim, {
            toValue: 1,
            duration: 1000,
            useNativeDriver: true,
        }).start();
    }, [fadeAnim]);

    return (
        <ImageBackground
            source={require('../assets/images/background-image.png')}
            style={styles.container}
        >
            <Header />
            <Animated.View style={[styles.mainContent, { opacity: fadeAnim }]}>
                <Text style={styles.mainText}>Ready to explore the world of crypto?</Text>
                <Text style={styles.subtitle}>Your hub for tracking and analyzing the crypto market with Crypto Cooker</Text>
                <TouchableOpacity
                    style={styles.button}
                    onPress={() => navigation.navigate('Register')}  // Navigate to the signup screen
                >
                    <Text style={styles.buttonText}>Get Started!</Text>
                </TouchableOpacity>

                {/* Log In Button */}
                <TouchableOpacity
                    style={styles.buttonSecondary}
                    onPress={() => navigation.navigate('Login')}  // Navigate to the login screen
                >
                    <Text style={styles.buttonSecondaryText}>Log In</Text>
                </TouchableOpacity>
            </Animated.View>
        </ImageBackground>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: theme.background,
    },
    mainContent: {
        alignItems: 'center',
        justifyContent: 'center',
        paddingHorizontal: 20,
        zIndex: 0,
    },
    mainText: {
        fontSize: 30,
        fontWeight: 'bold',
        color: theme.textPrimary,
        textAlign: 'center',
        marginBottom: 20,
        letterSpacing: 1,
        fontFamily: fonts.SemiBold,
    },
    subtitle: {
        fontSize: 18,
        color: theme.textSecondary,
        textAlign: 'center',
        marginBottom: 30,
        fontFamily: fonts.Regular,
    },
    button: {
        backgroundColor: theme.buttonBackground,
        paddingVertical: 15,
        paddingHorizontal: 50,
        borderRadius: theme.borderRadius,
        marginBottom: 10,
        borderColor: theme.buttonBackground,
        borderWidth: 1,
        width: '80%',
        alignItems: 'center',
        transition: 'background-color 0.3s',
    },
    buttonSecondary: {
        backgroundColor: 'transparent',
        borderColor: theme.primary,
        borderWidth: 2,
        paddingVertical: 15,
        paddingHorizontal: 50,
        borderRadius: theme.borderRadius,
        width: '80%',
        alignItems: 'center',
        transition: 'background-color 0.3s',
    },
    buttonText: {
        color: theme.white,
        fontSize: 15,
        fontFamily: fonts.SemiBold,
        textAlign: "center",
    },
    buttonSecondaryText: {
        fontSize: 15,
        color: theme.textPrimary,
        fontFamily: fonts.Bold,
    },
});

export default LandingScreen;
