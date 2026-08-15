import { useContext } from "react";
import { AuthContext } from "../context/AuthContext";

export default function useAuth() {

    return useContext(AuthContext);
}

//export function usePermissions(permission) {

//    const { user } = useAuth();

//    return user?.permissions?.includes(permission);
//}

//export function useRoles(role) {

//    const { user } = useAuth();

//    return user?.roles?.includes(role);
//}

//export function useRolePermissions(rolePermission) {

//    const { user } = useAuth();

//    return user?.rolePermissions?.includes(rolePermission);
//}

//export function useUsers(user) {

//    const { user1 } = useAuth();

//    return user1?.users?.includes(user);
//}

//export function useUserRoles(userRole) {

//    const { user } = useAuth();

//    return user?.userRoles?.includes(userRole);
//}

//export function useStates(state) {

//    const { user } = useAuth();

//    return user?.states?.includes(state);
//}

