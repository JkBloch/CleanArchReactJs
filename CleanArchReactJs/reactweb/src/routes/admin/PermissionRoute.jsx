import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import PermissionList from "../../pages/admin/permission/PermissionList";
import PermissionCreate from "../../pages/admin/permission/PermissionCreate";
import PermissionEdit from "../../pages/admin/permission/PermissionEdit";
import PermissionDetails from "../../pages/admin/permission/PermissionDetails";

function PermissionRoute()  {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                    </ProtectedRoute>
                } >   
                
                <Route path="/permissions"
                    element={
                        <ProtectedRoute roles={["Admin", "Employee"]}>
                            <PermissionList />
                        </ProtectedRoute>
                    } />
                <Route path="/permissions/create"
                    element={
                        <PermissionCreate />
                    } />
                <Route path="/permissions/:id"
                    element={
                        <PermissionDetails />
                    } />
                <Route path="/permissions/edit/:id"
                    element={
                        <PermissionEdit />
                    } />
            </Route>          
        </Routes>);
}
export default PermissionRoute;