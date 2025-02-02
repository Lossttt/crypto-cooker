import React from 'react';
import { View, StyleSheet, Image } from 'react-native';
import { theme } from '../shared/styles/theme';

const Header = () => {
  return (
    <View style={styles.container}>
      <Image source={require('../assets/favicon.png')} style={styles.logo} />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    justifyContent: 'center',
    alignItems: 'center',
    marginTop: 40,
    marginBottom: 100,
  },
  logo: {
    width: 120,
    height: 80,
    resizeMode: 'contain',
  },
});

export default Header;
