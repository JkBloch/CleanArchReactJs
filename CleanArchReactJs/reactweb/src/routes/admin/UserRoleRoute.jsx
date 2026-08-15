import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import UserRoleList from "../../pages/admin/userRole/UserRoleList";
import UserRoleCreate from "../../pages/admin/userRole/UserRoleCreate";
import UserRoleEdit from "../../pages/admin/userRole/UserRoleEdit";
import UserRoleDetails from "../../pages/admin/userRole/UserRoleDetails";


function UserRoleRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/userRoles"
                    element={<UserRoleList />
                    } />
                <Route path="/userRoles/create"
                    element={
                        <UserRoleCreate />
                    } />

                <Route path="/userRoles/:id"
                    element={
                        <UserRoleDetails />
                    } />
                <Route path="/userRoles/edit/:id"
                    element={
                        <UserRoleEdit />
                    } />

            </Route>
        </Routes>);
}

export default UserRoleRoute;