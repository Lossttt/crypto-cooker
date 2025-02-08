import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import LottieView from 'lottie-react-native';
import { theme } from '../../shared/styles/theme';
import { fonts } from '../../shared/styles/font';


interface LoadingScreenProps {
    title: string;
    message: string;
    animationUrl: any;
}

const LoadingScreen: React.FC<LoadingScreenProps> = ({ title, message, animationUrl }) => {
    return (
        <View style={styles.container}>
            <LottieView
                source={animationUrl}
                autoPlay
                loop
                style={styles.animation}
            />
            <Text style={styles.title}>{title}</Text>
            <Text style={styles.message}>{message}</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: theme.white,
        paddingHorizontal: 20,
    },
    animation: {
        width: 150,
        height: 150,
    },
    title: {
        fontSize: 24,
        color: theme.primaryColor,
        fontFamily: fonts.SemiBold,
        marginTop: 20,
    },
    message: {
        fontSize: 16,
        color: theme.secondaryColor,
        fontFamily: fonts.Regular,
        textAlign: 'center',
        marginTop: 10,
    },
});

export default LoadingScreen;
