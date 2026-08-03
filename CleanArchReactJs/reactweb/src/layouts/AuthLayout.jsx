import { Outlet } from "react-router-dom";

function AuthLayout() {

    return (
        <div className="container mt-5">

            <Outlet />

        </div>
    );
}

export default AuthLayout;