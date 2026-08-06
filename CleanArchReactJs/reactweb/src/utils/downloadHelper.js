export async function downloadExcel(filters) {
    const response =
        await exportEmployeesExcel(filters);

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
        "Permission.xlsx";

    link.click();

    window.URL.revokeObjectURL(url);
}