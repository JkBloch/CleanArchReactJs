import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import UserList from "../../pages/admin/user/UserList";
import UserCreate from "../../pages/admin/user/UserCreate";
import UserEdit from "../../pages/admin/user/UserEdit";
import UserDetails from "../../pages/admin/user/UserDetails";


function UserRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/users"
                    element={<UserList />
                    } />
                <Route path="/users/create"
                    element={
                        <UserCreate />
                    } />

                <Route path="/users/:id"
                    element={
                        <UserDetails />
                    } />
                <Route path="/users/edit/:id"
                    element={
                        <UserEdit />
                    } />

            </Route>
        </Routes>);
}

export default UserRoute;