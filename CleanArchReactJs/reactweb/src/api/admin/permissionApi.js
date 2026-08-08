//permissionApi
import apiClient from "../common/apiClient";

export const getPermissions = () =>
    apiClient.get("/Permission");

export const getPermission = id =>
    apiClient.get(`/Permission/${id}`);

export const createPermission = data =>
    apiClient.post("/Permission", data);

export const updatePermission = (id, data) =>
    apiClient.put(`/Permission/${id}`, data);

export const deletePermission = id =>
    apiClient.delete(`/Permission/${id}`);

export const restorePermission = id =>
    apiClient.post(`/Permission/${id}/restore`);

export const searchPermissions = data =>
    apiClient.post("/Permission/search", data);

export const deletePermanentPermissions = id =>
    apiClient.delete(`/Permission/${id}/deletepermanent`,);

