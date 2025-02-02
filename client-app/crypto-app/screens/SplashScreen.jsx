import React, { useEffect } from 'react';
import { View, StyleSheet, Text, Animated } from 'react-native';
import LottieView from 'lottie-react-native';

const SplashScreen = ({ navigation }) => {
    useEffect(() => {
        setTimeout(() => {
            navigation.replace('LandingPage');
        }, 3000);
    }, [navigation]);

    return (
        <View style={styles.container}>
            <LottieView
                source={require('../assets/animations/spash-screen-animation.json')}
                autoPlay
                loop={false}
                style={styles.animation}
            />
            <Text style={styles.title}>Crypto Cooker</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#FFFFFF',
    },
    animation: {
        width: 200,
        height: 200,
    },
    title: {
        fontSize: 24,
        fontWeight: 'bold',
        color: '#000',
        marginTop: 20,
    },
});

export default SplashScreen;
