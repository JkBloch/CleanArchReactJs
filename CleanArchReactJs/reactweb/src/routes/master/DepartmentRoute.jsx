import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import DepartmentList from "../../pages/master/department/DepartmentList";
import DepartmentCreate from "../../pages/master/department/DepartmentCreate";
import DepartmentEdit from "../../pages/master/department/DepartmentEdit";
import DepartmentDetails from "../../pages/master/department/DepartmentDetails";

function DepartmentRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/departments"
                    element={<DepartmentList />
                    } />
                <Route path="/departments/create"
                    element={
                        <DepartmentCreate />
                    } />

                <Route path="/departments/:id"
                    element={
                        <DepartmentDetails />
                    } />
                <Route path="/departments/edit/:id"
                    element={
                        <DepartmentEdit />
                    } />

            </Route>
        </Routes>);
}

export default DepartmentRoute;