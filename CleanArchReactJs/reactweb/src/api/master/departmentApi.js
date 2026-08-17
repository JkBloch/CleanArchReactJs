//departmentApi.js
import apiClient from "../common/apiClient";
const API_VERSION = "v1";
export const getDepartments = async () =>
    apiClient.get(`${API_VERSION}/Department`);

export const getDepartment = id =>
    apiClient.get(`${API_VERSION}/Department/${id}`);

export const createDepartment = data =>
    apiClient.post(`${API_VERSION}/Department`, data);

export const updateDepartment = (id, data) =>
    apiClient.put(`${API_VERSION}/Department/${id}`, data);

export const deleteDepartment = id =>
    apiClient.delete(`${API_VERSION}/Department/${id}`);

export const restoreDepartment = id =>
    apiClient.post(`${API_VERSION}/Department/${id}/restore`);

export const searchDepartments = data =>
    apiClient.post(`${API_VERSION}/Department/search`, data);

export const deletePermanentDepartments = id =>
    apiClient.delete(`${API_VERSION}/Department/${id}/deletepermanent`,);

