//departmentApi.js
import apiClient from "../common/apiClient";

export const getDepartments = async () =>
    apiClient.get("/Department");

export const getDepartment = id =>
    apiClient.get(`/Department/${id}`);

export const createDepartment = data =>
    apiClient.post("/Department", data);

export const updateDepartment = (id, data) =>
    apiClient.put(`/Department/${id}`, data);

export const deleteDepartment = id =>
    apiClient.delete(`/Department/${id}`);

export const restoreDepartment = id =>
    apiClient.post(`/Department/${id}/restore`);

export const searchDepartments = data =>
    apiClient.post("/Department/search", data);

export const deletePermanentDepartments = id =>
    apiClient.delete(`/Department/${id}/deletepermanent`,);

