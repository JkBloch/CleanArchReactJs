//authApi
import apiClient from "./apiClient";

export const login = (data) =>
    apiClient.post("/Auth/login", data);

export const register = (data) =>
    apiClient.post("/Auth/register", data);

export const refreshToken = (data) =>
    apiClient.post("/auth/refresh-token", data);

export const logout = () =>
    apiClient.post("/auth/logout");