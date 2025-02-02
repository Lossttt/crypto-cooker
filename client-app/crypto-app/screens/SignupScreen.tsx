import React from 'react';
import { View, Text, StyleSheet } from 'react-native';

const SignupScreen = () => {
    return (
        <View style={styles.container}>
            <Text style={styles.text}>Hello SignUp!</Text>
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
    text: {
        fontSize: 24,
        fontWeight: 'bold',
    },
});

export default SignupScreen;
