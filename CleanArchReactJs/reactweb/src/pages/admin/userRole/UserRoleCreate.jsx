import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createUserRole } from "../../../api/admin/userRoleApi";
import UserRoleForm from "./UserRoleForm";
import { notify } from "../../../services/notificationService";
import { getErrorMessage } from "../../../utils/errorHandling";

function UserRoleCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        roleId: "",
        userId: ""
    });



    async function save(userRole) {

        setLoading(true);

        try {

            await createUserRole(userRole);
            notify.success(
                "UserRole created successfully."
            );

            // navigate("/userRoles");

        }
        catch (error) {
            notify.error(
                getErrorMessage(error)
            );
            setInitialValues(userRole);


        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add UserRole</h2>

            <UserRoleForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default UserRoleCreate;