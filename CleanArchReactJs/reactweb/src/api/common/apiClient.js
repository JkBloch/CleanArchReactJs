//apiClient
import axios from "axios";
import { tokenStorage } from "../../utils/tokenHelper";
import { notify } from "../../services/notificationService";

const apiClient = axios.create({
    baseURL: import.meta.env.VITE_API_BASE_URL,
    headers: {
        "Content-Type": "application/json"
    }
});
//jwt tokent related code 
apiClient.interceptors.request.use(config => {

    config.headers.Authorization = `Bearer ${tokenStorage.getAccessToken()}`;
    return config;
});

apiClient.interceptors.response.use(

    response => response,

    error => {

        const status =
            error.response?.status;

        switch (status) {

            case 400:

                notify.error(

                    error.response.data.message
                    ??
                    "Bad Request"

                );

                break;

            case 401:

                notify.warning(

                    "Session expired."

                );

                tokenStorage.clear();

                window.location.href =
                    "/login";

                break;

            case 403:

                notify.error(

                    "Access denied."

                );

                break;

            case 404:

                notify.error(

                    "Resource not found."

                );

                break;

            case 500:

                notify.error(

                    "Internal server error."

                );

                break;

            default:

                notify.error(

                    getErrorMessage(error)

                );

                break;

        }

        return Promise.reject(error);

    }

);

export default apiClient;