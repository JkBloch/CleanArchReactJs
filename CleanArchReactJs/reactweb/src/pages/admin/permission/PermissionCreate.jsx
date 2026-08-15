import { useNavigate } from "react-router-dom";
import { useState } from "react"; 
import { createPermission } from "../.././../api/admin/permissionApi";
import PermissionForm from "../permission/PermissionForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function PermissionCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: ""
    });
   

    async function save(permission) {
        setLoading(true);
        try {
            await createPermission(permission);
            notify.success(
                "Permission created successfully."
            );
           // navigate("/permissions");

        }
        catch (error) {
            //notify.error(
            //    getErrorMessage(error)
            //);
            setInitialValues(permission);  
        }
        finally {
            setLoading(false);
        }
    }

    return (
        <div className="container">
            <h2>Add Permission</h2>
            <PermissionForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />
        </div>
    );
}

export default PermissionCreate;