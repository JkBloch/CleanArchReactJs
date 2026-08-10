//userRoleApi
import apiClient from "../common/apiClient";

export const getUserRoles = () =>
    apiClient.get("/UserRole");

export const getUserRole = id =>
    apiClient.get(`/UserRole/${id}`);

export const createUserRole = data =>
    apiClient.post("/UserRole", data);

export const updateUserRole = (id, data) =>
    apiClient.put(`/UserRole/${id}`, data);

export const deleteUserRole = id =>
    apiClient.delete(`/UserRole/${id}`);

export const restoreUserRole = id =>
    apiClient.post(`/UserRole/${id}/restore`);

export const searchUserRoles = data =>
    apiClient.post("/UserRole/search", data);

export const deletePermanentUserRoles = id =>
    apiClient.delete(`/UserRole/${id}/deletepermanent`,);

