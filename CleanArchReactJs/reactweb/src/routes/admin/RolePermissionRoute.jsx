import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import RolePermissionList from "../../pages/admin/rolePermission/RolePermissionList";
import RolePermissionCreate from "../../pages/admin/rolePermission/RolePermissionCreate";
import RolePermissionEdit from "../../pages/admin/rolePermission/RolePermissionEdit";
import RolePermissionDetails from "../../pages/admin/rolePermission/RolePermissionDetails";


function RolePermissionRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/rolePermissions"
                    element={<RolePermissionList />
                    } />
                <Route path="/rolePermissions/create"
                    element={
                        <RolePermissionCreate />
                    } />

                <Route path="/rolePermissions/:id"
                    element={
                        <RolePermissionDetails />
                    } />
                <Route path="/rolePermissions/edit/:id"
                    element={
                        <RolePermissionEdit />
                    } />
            </Route>
        </Routes>);
}

export default RolePermissionRoute;