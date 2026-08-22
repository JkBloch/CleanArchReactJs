export const getImageUrl = photoUrl => {

    if (!photoUrl)
        return null;

    // Already absolute URL
    if (
        photoUrl.startsWith("http://") ||
        photoUrl.startsWith("https://")
    ) {
        return photoUrl;
    }

    const apiUrl =
        import.meta.env.VITE_API_BASE_URL;

    // Remove /api
    const baseUrl =
        apiUrl.replace(/\/api\/?$/, "");

    return `${baseUrl}${photoUrl}`;
};