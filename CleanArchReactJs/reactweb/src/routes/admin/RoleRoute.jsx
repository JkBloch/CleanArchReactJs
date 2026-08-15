import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import RoleList from "../../pages/admin/role/RoleList";
import RoleCreate from "../../pages/admin/role/RoleCreate";
import RoleEdit from "../../pages/admin/role/RoleEdit";
import RoleDetails from "../../pages/admin/role/RoleDetails";

function RoleRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/roles"
                    element={<RoleList />
                    } />
                <Route path="/roles/create"
                    element={
                        <RoleCreate />
                    } />

                <Route path="/roles/:id"
                    element={
                        <RoleDetails />
                    } />
                <Route path="/roles/edit/:id"
                    element={
                        <RoleEdit />
                    } />

            </Route>
        </Routes>);
}

export default RoleRoute;