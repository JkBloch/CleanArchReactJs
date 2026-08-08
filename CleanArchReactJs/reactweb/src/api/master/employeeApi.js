//employeeApi
import apiClient from "./apiClient";

export const getEmployees = () =>
    apiClient.get("/employee");

export const getEmployee = id =>
    apiClient.get(`/employee/${id}`);

export const createEmployee = data =>
    apiClient.post("/employee", data);

export const updateEmployee = (id, data) =>
    apiClient.put(`/employee/${id}`, data);

export const deleteEmployee = id =>
    apiClient.delete(`/employee/${id}`);

export const restoreEmployee = id =>
    apiClient.post(`/employee/${id}/restore`);

export const searchEmployee = data =>
    apiClient.post("/employee/search", data);