const ACCESS_TOKEN = "accessToken";
const REFRESH_TOKEN = "refreshToken";
const USER = "user";

export const tokenStorage = {

    set(accessToken, refreshToken, user) {

        localStorage.setItem(ACCESS_TOKEN, accessToken);
        localStorage.setItem(REFRESH_TOKEN, refreshToken);
        localStorage.setItem(USER, JSON.stringify(user));
    },

    clear() {

        localStorage.removeItem(ACCESS_TOKEN);
        localStorage.removeItem(REFRESH_TOKEN);
        localStorage.removeItem(USER);
    },

    getAccessToken() {

        return localStorage.getItem(ACCESS_TOKEN);
    },

    getRefreshToken() {

        return localStorage.getItem(REFRESH_TOKEN);
    },

    getUser() {

        const user = localStorage.getItem(USER);

        return user ? JSON.parse(user) : null;
    }
};