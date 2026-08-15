import CityRoute from "./CityRoute";
import DepartmentRoute from "./DepartmentRoute";
import StateRoute from "./StateRoute";

function MasterRoute() {
    return (
        <>
            <StateRoute></StateRoute>
            <CityRoute></CityRoute>
            <DepartmentRoute></DepartmentRoute>
        </>);
}

export default MasterRoute;