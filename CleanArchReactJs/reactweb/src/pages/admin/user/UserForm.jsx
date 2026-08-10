import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa"; 
function UserForm({
    initialValues,
    onSubmit,
    loading
}) {

    const [user, setUser] = useState(initialValues);

    useEffect(() => {
        setUser(initialValues);
    }, [initialValues]);

    function handleChange(e) {

        let { name, value } = e.target;

        if (name == "isActive" || name == "isLocked") {
            value = e.target.checked;
        }

        setUser(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(user);
        setUser(user);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">

                <div className="col-md-6 mb-3">

                    <label>FirstName</label>

                    <input
                        className="form-control"
                        name="firstName"
                        value={user.firstName}
                        onChange={handleChange}
                        required
                    />

                </div>
                
                <div className="col-md-6 mb-3">

                    <label>LastName</label>

                    <input
                        className="form-control"
                        name="lastName"
                        value={user.lastName}
                        onChange={handleChange}
                        required
                    />

                </div>
                <div className="col-md-6 mb-3">

                    <label>UserName</label>

                    <input
                        className="form-control"
                        name="userName"
                        value={user.userName}
                        onChange={handleChange}
                        required
                    />

                </div>
                {
                 (user.id == null) && ( 
                     <div className="col-md-6 mb-3">

                    <label>Password</label>

                    <input
                        className="form-control"
                        name="password"
                        value={user.password}
                        onChange={handleChange}
                        required
                    />

                </div>)
                }
                {
                    (user.id == null) && ( 
                <div className="col-md-6 mb-3">

                    <label>ConfirmPassword</label>

                    <input
                        className="form-control"
                        name="confirmPassword"
                        value={user.confirmPassword}
                        onChange={handleChange}
                        required
                    />

                </div>
                    )
                }
                <div className="col-md-6 mb-3">

                    <label>Email</label>

                    <input
                        className="form-control"
                        name="email"
                        value={user.email}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>PhoneNumber</label>

                    <input
                        className="form-control"
                        name="phoneNumber"
                        value={user.phoneNumber}
                        onChange={handleChange}
                        required
                    />

                </div>
                <div className="col-md-6 mb-3">
                    <div className="row ms-2 mt-4">
                    <div className="col-md-3 form-check">
                            <input
                                type="checkbox"
                                className="form-check-input"
                                name="isActive"
                                checked={user.isActive}
                                value={user.isActive}
                                onChange={handleChange} />
                    <label
                        className="form-check-label"
                        htmlFor="isLocked"
                    >
                        IsActive
                    </label> 
                    </div>
                        <div className="col form-check">
                    <input
                        type="checkbox"
                        className="form-check-input "
                                name="isLocked"
                                checked={user.isLocked}
                        value={user.isLocked}
                        onChange={handleChange} />
                    <label
                        className="form-check-label"
                        htmlFor="isLocked"
                    >
                        IsLocked
                    </label>
                                        </div>
                    </div>

                </div>
                <div className="col-md-6 mb-3">
                   

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
                <Link to="/users"
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

export default UserForm;