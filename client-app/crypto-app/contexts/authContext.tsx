import React, { createContext, useContext, useState, ReactNode } from 'react';

interface AuthContextProps {
    email: string;
    setEmail: (email: string) => void;
    code: string;
    setCode: (code: string) => void;
}

const AuthContext = createContext<AuthContextProps | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }): JSX.Element => {
    const [email, setEmail] = useState<string>('');
    const [code, setCode] = useState<string>('');

    return (
        <AuthContext.Provider value={{ email, setEmail, code, setCode }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuthContext = (): AuthContextProps => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuthContext must be used within an AuthProvider');
    }
    return context;
};
