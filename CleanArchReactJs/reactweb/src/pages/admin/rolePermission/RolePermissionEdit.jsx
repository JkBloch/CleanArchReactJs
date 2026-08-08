import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";

import RolePermissionForm from "../rolePermission/RolePermissionForm";

import {
    getRolePermission,
    updateRolePermission
} from "../.././../api/admin/rolePermissionApi";
import { notify } from "../../../services/notificationService";

function RolePermissionEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [rolePermission, setRolePermission] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadRolePermission();

    }, []);

    async function loadRolePermission() {

        const response = await getRolePermission(id);

        setRolePermission(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateRolePermission(id, data);
            notify.success(

                "RolePermission updated successfully."

            );


            navigate("/rolePermissions");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit RolePermission</h2>

            <RolePermissionForm
                initialValues={rolePermission}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default RolePermissionEdit;