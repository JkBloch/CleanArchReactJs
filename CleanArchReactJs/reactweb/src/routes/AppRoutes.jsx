import { Routes, Route } from "react-router-dom";

import AppLayout from "../layouts/AppLayout";
import AuthLayout from "../layouts/AuthLayout";

import ProtectedRoute from "../routes/ProtectedRoute";
import PublicRoute from "../routes/PublicRoute";

import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";

import Dashboard from "../pages/dashboard/Dashboard";
import PermissionList from "../pages/admin/permission/PermissionList";
import PermissionCreate from "../pages/admin/permission/PermissionCreate";
import PermissionEdit from "../pages/admin/permission/PermissionEdit";
import PermissionDetails from "../pages/admin/permission/PermissionDetails";
import RoleList from "../pages/admin/role/RoleList";
import RoleCreate from "../pages/admin/role/RoleCreate";
import RoleEdit from "../pages/admin/role/RoleEdit";
import RoleDetails from "../pages/admin/role/RoleDetails";
function AppRoutes() {
        

    return (
        <Routes>
            <Route element={<AppLayout />} >
                <Route
                    path="/permissions"
                    element={<PermissionList />
                    }
                />
                <Route
                    path="/permissions/create"
                    element={
                        <PermissionCreate />
                    }
                />

                <Route
                    path="/permissions/:id"
                    element={
                        <PermissionDetails />
                    }
                />
                <Route
                    path="/permissions/edit/:id"
                    element={
                        <PermissionEdit />
                    }
                />
                <Route
                    path="/roles"
                    element={<RoleList />
                    }
                />
                <Route
                    path="/roles/create"
                    element={
                        <RoleCreate />
                    }
                />

                <Route
                    path="/roles/:id"
                    element={
                        <RoleDetails />
                    }
                />
                <Route
                    path="/roles/edit/:id"
                    element={
                        <RoleEdit />
                    }
                />
                <Route
                    path="/dashboard"
                    element={<Dashboard />
                    }
                />
                <Route
                    path="/"
                    element={<Dashboard />
                    }
                />

            </Route>
        </Routes>


        //<Routes>

        //    <Route element={<AuthLayout />}>

        //        <Route
        //            path="/login"
        //            element={
        //                <PublicRoute>
        //                    <Login />
        //                </PublicRoute>
        //            }
        //        />

        //        <Route
        //            path="/register"
        //            element={
        //                <PublicRoute>
        //                    <Register />
        //                </PublicRoute>
        //            }
        //        />

        //    </Route>

        //    <Route
        //        element={
        //            <ProtectedRoute>

        //                <AppLayout />

        //            </ProtectedRoute>
        //        }
        //    >

        //        <Route
        //            path="/"
        //            element={<Dashboard />}
        //        />

        //        <Route
        //            path="/employees"
        //            element={
        //                <ProtectedRoute roles={["Admin", "HR"]}>
        //                    <EmployeeList />
        //                </ProtectedRoute>
        //            }
        //        />

        //        <Route
        //            path="/employees/create"
        //            element={
        //                <ProtectedRoute roles={["Admin", "HR"]}>
        //                    <EmployeeCreate />
        //                </ProtectedRoute>
        //            }
        //        />

        //        <Route
        //            path="/employees/edit/:id"
        //            element={
        //                <ProtectedRoute roles={["Admin", "HR"]}>
        //                    <EmployeeEdit />
        //                </ProtectedRoute>
        //            }
        //        />

        //        <Route
        //            path="/employees/:id"
        //            element={
        //                <ProtectedRoute>
        //                    <EmployeeDetails />
        //                </ProtectedRoute>
        //            }
        //        />

        //        <Route
        //            path="/employees"
        //            element={

        //                <PermissionList />

        //            }
        //        />

        //        <Route
        //            path="/employees/create"
        //            element={


        //                    <PermissionCreate />
                       
        //            }
        //        />

        //        <Route
        //            path="/employees/edit/:id"
        //            element={

        //                <PermissionEdit />

        //            }
        //        />

        //        <Route
        //            path="/employees/:id"
        //            element={
        //                <ProtectedRoute>
        //                    <PermissionDetails />
        //                </ProtectedRoute>
        //            }
        //        />
        //    </Route>

        //</Routes>

    );
}

export default AppRoutes;