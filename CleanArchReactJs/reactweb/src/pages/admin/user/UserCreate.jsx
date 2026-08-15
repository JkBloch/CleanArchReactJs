import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createUser } from "../.././../api/admin/userApi";
import UserForm from "../user/UserForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";
function UserCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        firstName: "",
        lastName: "",
        userName: "",
        email: "",
        password: "",
        confirmPassword: "",
        phoneNumber: "",
        isActive: false,
        isLocked: false,
    });
    async function save(user) {

        setLoading(true);

        try {

            await createUser(user);
            notify.success(
                "User created successfully."
            );

            navigate("/users");

        }
        catch (error) {
            //notify.error( getErrorMessage(error) );
            setInitialValues(user); 
        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add User</h2>

            <UserForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default UserCreate;