//userApi.js
import apiClient from "../common/apiClient";

export const getUsers = async () =>
    apiClient.get("/User");

export const getUser = id =>
    apiClient.get(`/User/${id}`);

export const createUser = data =>
    apiClient.post("/User", data);

export const updateUser = (id, data) =>
    apiClient.put(`/User/${id}`, data);

export const deleteUser = id =>
    apiClient.delete(`/User/${id}`);

export const restoreUser = id =>
    apiClient.post(`/User/${id}/restore`);

export const searchUsers = data =>
    apiClient.post("/User/search", data);

export const deletePermanentUsers = id =>
    apiClient.delete(`/User/${id}/deletepermanent`,);

