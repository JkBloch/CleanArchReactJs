import { Navigate } from "react-router-dom";
import useAuth from "../hooks/useAuth";

function ProtectedRoute({ children, roles = [] }) {

    const { loading, isAuthenticated, user } = useAuth();

    if (loading)
        return <div>Loading...</div>;

    if (!isAuthenticated)
        return <Navigate to="/login" replace />;

    if (
        roles.length > 0 &&
        !roles.includes(user.role)
    ) {
        return <Navigate to="/unauthorized" replace />;
    }

    return children;
}

export default ProtectedRoute;