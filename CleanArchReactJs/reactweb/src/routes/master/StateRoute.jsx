import { Routes, Route } from "react-router-dom";
import ProtectedRoute from "../../routes/ProtectedRoute";
import AppLayout from "../../layouts/AppLayout";
import StateList from "../../pages/master/state/StateList";
import StateCreate from "../../pages/master/state/StateCreate";
import StateEdit from "../../pages/master/state/StateEdit";
import StateDetails from "../../pages/master/state/StateDetails";

function StateRoute() {
    return (
        <Routes>
            <Route element={
                <ProtectedRoute>
                    <AppLayout />

                </ProtectedRoute>
            } >

                <Route path="/states"
                    element={<StateList />
                    } />
                <Route path="/states/create"
                    element={
                        <StateCreate />
                    } />

                <Route path="/states/:id"
                    element={
                        <StateDetails />
                    } />
                <Route path="/states/edit/:id"
                    element={
                        <StateEdit />
                    } />

            </Route>
        </Routes>);
}

export default StateRoute;