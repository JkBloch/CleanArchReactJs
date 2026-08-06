import axios from "axios";
const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});

export const exportPermissionsExcel = request =>
    apiClient.post(
        "/export/permissions/excel",
        request,
        {
            responseType: "blob"
        })

export async function downloadExcel(filters) {
    const response =
        await exportPermissionsExcel(filters);

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

    link.download =
        "Permissions.xlsx";

    link.click();

    window.URL.revokeObjectURL(url);
}


export const exportPermissionsPdf = filters =>
    apiClient.post(
        "/export/permissions/pdf",
        filters,
        {
            responseType: "blob"
        });

export async function downloadPdf(filters) {
    const response =
        await exportPermissionsPdf(filters);

    const blob =
        new Blob([response.data], {
            type: "application/pdf"
        });

    const url =
        window.URL.createObjectURL(blob);

    const link =
        document.createElement("a");

    link.href = url;

    link.download = "PermissionReport.pdf";

    link.click();

    window.URL.revokeObjectURL(url);
}

