import PermissionRoute from "./PermissionRoute";
import RoleRoute from "./RoleRoute";
import RolePermissionRoute from "./RolePermissionRoute";
import UserRoute from "./UserRoute";
import UserRoleRoute from "./UserRoleRoute";
const AdminRoute = () => {
    return (
        <>
            <PermissionRoute></PermissionRoute>
            <RoleRoute></RoleRoute>
            <RolePermissionRoute></RolePermissionRoute>
            <UserRoute></UserRoute>
            <UserRoleRoute></UserRoleRoute>
        </>

    );
}

export default AdminRoute;