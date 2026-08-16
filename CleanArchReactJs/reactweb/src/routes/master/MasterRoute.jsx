import CityRoute from "./CityRoute";
import DepartmentRoute from "./DepartmentRoute";
import EmployeeRoute from "./EmployeeRoute";
import StateRoute from "./StateRoute";

function MasterRoute() {
    return (
        <>
            <StateRoute></StateRoute>
            <CityRoute></CityRoute>
            <DepartmentRoute></DepartmentRoute>
            <EmployeeRoute></EmployeeRoute>
        </>);
}

export default MasterRoute;