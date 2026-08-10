import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import RoleForm from "../role/RoleForm";
import {getRole,updateRole} from "../.././../api/admin/roleApi";
import { notify } from "../../../services/notificationService";

function RoleEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [role, setRole] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadRole();

    }, []);

    async function loadRole() {

        const response = await getRole(id);

        setRole(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateRole(id, data);
            notify.success(

                "Role updated successfully."

            );


            navigate("/roles");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit Role</h2>

            <RoleForm
                initialValues={role}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default RoleEdit;