//stateApi.js
import apiClient from "../common/apiClient";

export const getStates = async () =>
    apiClient.get("/State");

export const getState = id =>
    apiClient.get(`/State/${id}`);

export const createState = data =>
    apiClient.post("/State", data);

export const updateState = (id, data) =>
    apiClient.put(`/State/${id}`, data);

export const deleteState = id =>
    apiClient.delete(`/State/${id}`);

export const restoreState = id =>
    apiClient.post(`/State/${id}/restore`);

export const searchStates = data =>
    apiClient.post("/State/search", data);

export const deletePermanentStates = id =>
    apiClient.delete(`/State/${id}/deletepermanent`,);

