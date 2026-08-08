//rolePermissionApi
import apiClient from "../common/apiClient";

export const getRolePermissions = () =>
    apiClient.get("/RolePermission");

export const getRolePermission = id =>
    apiClient.get(`/RolePermission/${id}`);

export const createRolePermission = data =>
    apiClient.post("/RolePermission", data);

export const updateRolePermission = (id, data) =>
    apiClient.put(`/RolePermission/${id}`, data);

export const deleteRolePermission = id =>
    apiClient.delete(`/RolePermission/${id}`);

export const restoreRolePermission = id =>
    apiClient.post(`/RolePermission/${id}/restore`);

export const searchRolePermissions = data =>
    apiClient.post("/RolePermission/search", data);

export const deletePermanentRolePermissions = id =>
    apiClient.delete(`/RolePermission/${id}/deletepermanent`,);

