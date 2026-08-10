import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createRolePermission } from "../.././../api/admin/rolePermissionApi";
import RolePermissionForm from "../rolePermission/RolePermissionForm";
import { notify } from "../../../services/notificationService";
import { getErrorMessage } from "../../../utils/errorHandling";

function RolePermissionCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        roleId: "",
        permissionId: ""
    });

    

    async function save(rolePermission) {

        setLoading(true);

        try {

            await createRolePermission(rolePermission);
            notify.success(
                "RolePermission created successfully."
            );

           // navigate("/rolePermissions");

        }
        catch (error) {
            notify.error(
                getErrorMessage(error)
            );
            setInitialValues(rolePermission);
           

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add RolePermission</h2>

            <RolePermissionForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default RolePermissionCreate;