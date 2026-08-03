import { createContext, useEffect, useState } from "react";
import { login as loginApi } from "../api/authApi";
import { tokenStorage } from "../utils/tokenHelper";

export const AuthContext = createContext(null);

export function AuthProvider({ children }) {

    const [user, setUser] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        const storedUser = tokenStorage.getUser();

        if (storedUser) {

            setUser(storedUser);
        }

        setLoading(false);

    }, []);

    const login = async (credentials) => {
        try {
            const response = await loginApi(credentials);

            const data = response.data;
            if (!data.success) {
                throw new Error(data.message);
            }
            tokenStorage.set(
                data.accessToken,
                data.refreshToken,
                data.user
            );
            setUser(data.user);

        } catch (error) {
            throw new Error(
                error.response?.data?.message ?? "Login failed."
            );
        }
    };

    const logout = () => {

        tokenStorage.clear();

        setUser(null);
    };

    return (

        <AuthContext.Provider
            value={{
                user,
                loading,
                login,
                logout,
                isAuthenticated: !!user
            }}
        >
            {children}
        </AuthContext.Provider>

    );
}