import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import UserRoleForm from "./UserRoleForm";
import { getUserRole, updateUserRole } from "../../../api/admin/userRoleApi";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function UserRoleEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [userRole, setUserRole] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadUserRole();

    }, []);

    async function loadUserRole() {

        const response = await getUserRole(id);

        setUserRole(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateUserRole(id, data);
            notify.success(
                "UserRole updated successfully."
            );
            navigate("/userRoles");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setUserRole(data);
            setLoading(false);
        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit UserRole</h2>

            <UserRoleForm
                initialValues={userRole}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default UserRoleEdit;