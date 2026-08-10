import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa";
import { getRoles } from "../../../api/admin/roleApi";
import { notify } from "../../../services/notificationService";
import { getUsers } from "../../../api/admin/userApi";
function UserRoleForm({
    initialValues,
    onSubmit,
    loading
}) {
    const [roles, setRoles] = useState([]);
    const [users, setUsers] = useState([]);
    const [userRole, setUserRole] = useState(initialValues);

    useEffect(() => {
        loadRole();
        loadUser();
        setUserRole(initialValues);
    }, [initialValues]);
    const loadRole = async () => {
        try {
            var roleResponse = await getRoles();
            setRoles(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role data");
        }
    };
    const loadUser = async () => {
        try {
            var roleResponse = await getUsers();
            setUsers(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role Users");
        }
    };
    function handleChange(e) {

        const { name, value } = e.target;

        setUserRole(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(userRole);
        setUserRole(userRole);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">

                <div className="col-md-6 mb-3">
                    <label>User</label>
                    <select
                        className="form-select"
                        name="userId"
                        value={userRole.userId}
                        onChange={handleChange}
                    >
                        <option value="">-- Select User --</option>

                        {users.map(user => (
                            <option key={user.id} value={user.id}>
                                {user.userName}
                            </option>
                        ))}
                    </select>

                </div>
                <div className="col-md-6 mb-3"> 

                    <label>Role</label>
                    <select
                        className="form-select"
                        name="roleId"
                        value={userRole.roleId}
                        onChange={handleChange}
                    >
                        <option value="">-- Select Role --</option>

                        {roles.map(role => (
                            <option key={role.id} value={role.id}>
                                {role.name}
                            </option>
                        ))}
                    </select>

                </div>

            </div>
            <div>
                <button
                    className="icon-btn-success icon-btn"
                    disabled={loading}
                >
                    <span className="icon-section">
                        <FaRegSave></FaRegSave>
                    </span>
                    <span className="text-section">
                        Save
                    </span>
                </button>
                <Link to="/userRoles"
                    className="icon-btn-info icon-btn no-underline" >
                    <span className="icon-section">
                        <FaArrowLeft></FaArrowLeft>
                    </span>
                    <span className="text-section">
                        Go Back
                    </span>
                </Link>
            </div>
        </form>

    );
}

export default UserRoleForm;