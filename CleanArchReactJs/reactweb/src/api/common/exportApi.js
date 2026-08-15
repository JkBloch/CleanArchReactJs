//exportApi
import axios from "axios";
const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});

export async function downloadExcel(filters,reportName) {
    var response = "";
    switch (reportName) {
        case "PermissionReport":
            response = await exportPermissionsExcel(filters);
            break;
        case "RoleReport":
            response = await exportRolesExcel(filters);
            break;
        case "RolePermissionReport":
            response = await exportRolePermissionsExcel(filters);
            break;     
        case "UserReport":
            response = await exportUsersExcel(filters);
            break;    
        case "StateReport":
            response = await exportRolesExcel(filters);
            break;
        default:
    }


    const blob =
        new Blob(
            [response.data],
            {
                type:
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            });

    const url =
        window.URL.createObjectURL(blob);

    const link =
        document.createElement("a");

    link.href = url;

    link.download = reportName +".xlsx";

    link.click();

    window.URL.revokeObjectURL(url);
}
export async function downloadPdf(filters, reportName) {

    var response = {};
    switch (reportName) {
        case "PermissionReport":
            response = await exportPermissionsPdf(filters);
            break;
        case "RoleReport":
            response = await exportRolesPdf(filters);
            break;
        case "RolePermissionReport":
            response = await exportRolePermissionsPdf(filters);
            break;
        case "UserReport":
            response = await exportUsersPdf(filters);
            break;
        case "StateReport":
            response = await exportRolesPdf(filters);
            break;
        default:
    }


    const blob =
        new Blob([response.data], {
            type: "application/pdf"
        });

    const url =
        window.URL.createObjectURL(blob);

    const link =
        document.createElement("a");

    link.href = url;

    link.download = reportName +".pdf";

    link.click();

    window.URL.revokeObjectURL(url);
}

export const exportPermissionsExcel = request =>
    apiClient.post(
        "/export/permissions/excel",
        request,
        {
            responseType: "blob"
        })

export const exportPermissionsPdf = filters =>
    apiClient.post(
        "/export/permissions/pdf",
        filters,
        {
            responseType: "blob"
        });

export const exportRolesExcel = request =>
    apiClient.post(
        "/export/roles/excel",
        request,
        {
            responseType: "blob"
        })

export const exportRolesPdf = filters =>
    apiClient.post(
        "/export/roles/pdf",
        filters,
        {
            responseType: "blob"
        });

export const exportRolePermissionsExcel = request =>
    apiClient.post(
        "/export/rolePermissions/excel",
        request,
        {
            responseType: "blob"
        })

export const exportRolePermissionsPdf = filters =>
    apiClient.post(
        "/export/rolePermissions/pdf",
        filters,
        {
            responseType: "blob"
        });

export const exportUsersExcel = request =>
    apiClient.post(
        "/export/users/excel",
        request,
        {
            responseType: "blob"
        })

export const exportUsersPdf = filters =>
    apiClient.post(
        "/export/users/pdf",
        filters,
        {
            responseType: "blob"
        });

export const exportUserRolesExcel = request =>
    apiClient.post(
        "/export/userRoles/excel",
        request,
        {
            responseType: "blob"
        })

export const exportUserRolesPdf = filters =>
    apiClient.post(
        "/export/userRoles/pdf",
        filters,
        {
            responseType: "blob"
        });

export const exportStatesExcel = request =>
    apiClient.post(
        "/export/states/excel",
        request,
        {
            responseType: "blob"
        })

export const exportStatesPdf = filters =>
    apiClient.post(
        "/export/states/pdf",
        filters,
        {
            responseType: "blob"
        });
