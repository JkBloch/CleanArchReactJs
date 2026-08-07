import { useContext } from "react";

import { AuthContext } from "../context/AuthContext";

export default function useAuth() {

    return useContext(AuthContext);
}

export function usePermission(permission) {

    const { user } = useAuth();

    return user?.permissions?.includes(permission);
}

export function useRole(role) {

    const { user } = useAuth();

    return user?.roles?.includes(role);
}
