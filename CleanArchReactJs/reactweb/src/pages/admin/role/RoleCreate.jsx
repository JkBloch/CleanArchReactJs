import { useNavigate } from "react-router-dom";
import { useState } from "react";

import { createRole } from "../.././../api/roleApi";

import RoleForm from "../role/RoleForm";
import { notify } from "../../../services/notificationService";
import { getErrorMessage } from "../../../utils/errorHandling";
function RoleCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: ""
    });

    //const initialValues = {
    //    code: "",
    //    name: ""
    //};

    async function save(role) {

        setLoading(true);

        try {

            await createRole(role);
            notify.success(
                "Role created successfully."
            );

            navigate("/roles");

        }
        catch (error) {
            notify.error(
                getErrorMessage(error)
            );
            setInitialValues(role);
            //alert(getErrorMessage(error))
            //    error.response?.data?.message ??
            //    "Unable to create role."
            //);

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add Role</h2>

            <RoleForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default RoleCreate;