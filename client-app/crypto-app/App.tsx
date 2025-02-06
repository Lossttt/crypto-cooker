import React, { useState, useEffect } from 'react';
import { NavigationContainer } from '@react-navigation/native';
import Routes from './navigation/Routes';
import { loadFonts } from './config/config.fonts';

export default function App() {
  const [fontsLoaded, setFontsLoaded] = useState(false);

  useEffect(() => {
    const loadAsyncFonts = async () => {
      await loadFonts();
      setFontsLoaded(true);
    };

    loadAsyncFonts();
  }, []);

  return (
    <NavigationContainer>
      <Routes />
    </NavigationContainer>
  );
}