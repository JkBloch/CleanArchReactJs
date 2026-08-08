//authApi
import apiClient from "./apiClient";

export const login = (data) =>
    apiClient.post("/auth/login", data);

export const register = (data) =>
    apiClient.post("/auth/register", data);

export const refreshToken = (data) =>
    apiClient.post("/auth/refresh-token", data);

export const logout = () =>
    apiClient.post("/auth/logout");