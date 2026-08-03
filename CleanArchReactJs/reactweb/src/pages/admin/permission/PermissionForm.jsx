import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa";
function PermissionForm({
    initialValues,
    onSubmit,
    loading
}) {

    const [permission, setPermission] = useState(initialValues);

    useEffect(() => {
        setPermission(initialValues);
    }, [initialValues]);

    function handleChange(e) {

        const { name, value } = e.target;

        setPermission(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(permission);
        setPermission(permission);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">

                <div className="col-md-6 mb-3">

                    <label>Code</label>

                    <input
                        className="form-control"
                        name="code"
                        value={permission.code}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Name</label>

                    <input
                        className="form-control"
                        name="name"
                        value={permission.name}
                        onChange={handleChange}
                        required
                    />

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
            <Link to="/permissions"
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

export default PermissionForm;