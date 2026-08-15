import { Routes, Route } from "react-router-dom";
import AppLayout from "../layouts/AppLayout";
import AuthLayout from "../layouts/AuthLayout";
import ProtectedRoute from "../routes/ProtectedRoute";
import PublicRoute from "../routes/PublicRoute";
import Login from "../pages/auth/Login";
import Register from "../pages/auth/Register";

import Dashboard from "../pages/dashboard/Dashboard";
import AdminRoute from "./admin/AdminRoute";
import MasterRoute from "./master/MasterRoute";
function AppRoutes() {
        

    return (
        <>
        <Routes>

            <Route element={<AuthLayout />}>

                <Route
                    path="/login"
                    element={
                        <PublicRoute>
                            <Login />
                        </PublicRoute>
                    }
                />

                <Route
                    path="/register"
                    element={
                        <PublicRoute>
                            <Register />
                        </PublicRoute>
                    }
                />

            </Route>
            <Route
                element={
                    <ProtectedRoute>

                        <AppLayout />

                    </ProtectedRoute>
                }
            >
           
                  
                <Route roles={["Admin","HR"] }
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

            <AdminRoute></AdminRoute>
            <MasterRoute></MasterRoute>
        </>
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