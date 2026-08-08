//roleApi.js
import apiClient from "../common/apiClient";

export const getRoles = () =>
    apiClient.get("/Role");

export const getRole = id =>
    apiClient.get(`/Role/${id}`);

export const createRole = data =>
    apiClient.post("/Role", data);

export const updateRole = (id, data) =>
    apiClient.put(`/Role/${id}`, data);

export const deleteRole = id =>
    apiClient.delete(`/Role/${id}`);

export const restoreRole = id =>
    apiClient.post(`/Role/${id}/restore`);

export const searchRoles = data =>
    apiClient.post("/Role/search", data);

export const deletePermanentRoles = id =>
    apiClient.delete(`/Role/${id}/deletepermanent`,);

