import CityRoute from "./CityRoute";
import StateRoute from "./StateRoute";

function MasterRoute() {
    return (
        <>
            <StateRoute></StateRoute>
            <CityRoute></CityRoute>
        </>);
}

export default MasterRoute;