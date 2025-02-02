import React from 'react';
import { NavigationContainer } from '@react-navigation/native';
import Routes from './navigation/Routes';

export default function App() {
  return (
    <NavigationContainer>
      <Routes />
    </NavigationContainer>
  );
}
