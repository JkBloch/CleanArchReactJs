import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa";
import { getRoles} from "../../../api/admin/roleApi";
import { notify } from "../../../services/notificationService";
import { getPermissions } from "../../../api/admin/permissionApi";
function RolePermissionForm({
    initialValues,
    onSubmit,
    loading
}) {
    const [roles, setRoles] = useState([]);
    const [permissions, setPermissions] = useState([]);
    const [rolePermission, setRolePermission] = useState(initialValues);

    useEffect(() => {
        loadRole();
        loadPermission();
        setRolePermission(initialValues);
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
    const loadPermission = async () => {
        try {
            var roleResponse = await getPermissions();
            setPermissions(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role Permissions");
        }
    };
    function handleChange(e) {

        const { name, value } = e.target;

        setRolePermission(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(rolePermission);
        setRolePermission(rolePermission);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">

                <div className="col-md-6 mb-3">
                    {/*<label className="form-label">*/}
                    {/*    Role <span className="text-danger">*</span>*/}
                    {/*</label>*/}

                  
                    <label>Role</label>
                    <select
                        className="form-select"
                        name="roleId"
                        value={rolePermission.roleId}
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

                <div className="col-md-6 mb-3">
                    <label>Permission</label>
                    <select
                        className="form-select"
                        name="permissionId"
                        value={rolePermission.permissionId}
                        onChange={handleChange}
                    >
                        <option value="">-- Select Permission --</option>

                        {permissions.map(permission => (
                            <option key={permission.id} value={permission.id}>
                                {permission.name}
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
                <Link to="/rolePermissions"
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

export default RolePermissionForm;