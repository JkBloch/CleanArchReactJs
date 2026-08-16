import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import EmployeeList from "../../pages/master/employee/EmployeeList";
import EmployeeCreate from "../../pages/master/employee/EmployeeCreate";
import EmployeeEdit from "../../pages/master/employee/EmployeeEdit";
import EmployeeDetails from "../../pages/master/employee/EmployeeDetails";

function EmployeeRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/employees"
                    element={<EmployeeList />
                    } />
                <Route path="/employees/create"
                    element={
                        <EmployeeCreate />
                    } />

                <Route path="/employees/:id"
                    element={
                        <EmployeeDetails />
                    } />
                <Route path="/employees/edit/:id"
                    element={
                        <EmployeeEdit />
                    } />

            </Route>
        </Routes>);
}

export default EmployeeRoute;