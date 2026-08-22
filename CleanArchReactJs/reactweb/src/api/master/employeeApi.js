//employeeApi.js
import apiClient from "../common/apiClient";

export const getEmployees = async () =>
    apiClient.get("/Employee");

export const getEmployee = id =>
    apiClient.get(`/Employee/${id}`);

export const createEmployee = data =>
    apiClient.post("/Employee", data);

export const updateEmployee = (id, data) =>
    apiClient.put(`/Employee/${id}`, data);

export const deleteEmployee = id =>
    apiClient.delete(`/Employee/${id}`);

export const restoreEmployee = id =>
    apiClient.post(`/Employee/${id}/restore`);

export const searchEmployees = data =>
    apiClient.post("/Employee/search", data);

export const deletePermanentEmployees = id =>
    apiClient.delete(`/Employee/${id}/deletepermanent`,);

export const uploadEmployeePhoto = (id, file) => {

    const formData = new FormData();

    formData.append("photo", file);

    return apiClient.post(
        `/employee/${id}/photo`,
        formData,
        {
            headers: {
                "Content-Type": "multipart/form-data"
            }
        }
    );
};
export const deleteEmployeePhoto = id =>
    apiClient.delete(
        `/employee/${id}/photo`
    );