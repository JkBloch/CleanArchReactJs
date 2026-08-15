import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import CityList from "../../pages/master/city/CityList";
import CityCreate from "../../pages/master/city/CityCreate";
import CityEdit from "../../pages/master/city/CityEdit";
import CityDetails from "../../pages/master/city/CityDetails";

function CityRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/cities"
                    element={<CityList />
                    } />
                <Route path="/cities/create"
                    element={
                        <CityCreate />
                    } />

                <Route path="/cities/:id"
                    element={
                        <CityDetails />
                    } />
                <Route path="/cities/edit/:id"
                    element={
                        <CityEdit />
                    } />

            </Route>
        </Routes>);
}

export default CityRoute;