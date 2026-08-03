import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import PermissionForm from "../permission/PermissionForm";

import {
    getPermission,
    updatePermission
} from "../.././../api/permissionApi";
import { notify } from "../../../services/notificationService";

function PermissionEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [permission, setPermission] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadPermission();

    }, []);

    async function loadPermission() {

        const response = await getPermission(id);

        setPermission(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updatePermission(id, data);
            notify.success(

                "Permission updated successfully."

            );


            navigate("/permissions");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit Permission</h2>

            <PermissionForm
                initialValues={permission}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default PermissionEdit;