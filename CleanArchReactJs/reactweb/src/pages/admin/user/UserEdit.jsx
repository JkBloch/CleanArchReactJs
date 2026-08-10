import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import UserForm from "../user/UserForm";
import {getUser,updateUser} from "../.././../api/admin/userApi";
import { notify } from "../../../services/notificationService";

function UserEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [user, setUser] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadUser();

    }, []);

    async function loadUser() {

        const response = await getUser(id);

        setUser(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateUser(id, data);
            notify.success(

                "User updated successfully."

            );


            navigate("/users");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit User</h2>

            <UserForm
                initialValues={user}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default UserEdit;