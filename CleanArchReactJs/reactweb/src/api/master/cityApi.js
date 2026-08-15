//cityApi.js
import apiClient from "../common/apiClient";

export const getCities = async () =>
    apiClient.get("/City");

export const getCity = id =>
    apiClient.get(`/City/${id}`);

export const createCity = data =>
    apiClient.post("/City", data);

export const updateCity = (id, data) =>
    apiClient.put(`/City/${id}`, data);

export const deleteCity = id =>
    apiClient.delete(`/City/${id}`);

export const restoreCity = id =>
    apiClient.post(`/City/${id}/restore`);

export const searchCities = data =>
    apiClient.post("/City/search", data);

export const deletePermanentCities = id =>
    apiClient.delete(`/City/${id}/deletepermanent`,);

