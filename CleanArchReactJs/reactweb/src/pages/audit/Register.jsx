import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { register } from "../../api/common/authApi";

function Register() {

    const navigate = useNavigate();

    const [error, setError] = useState("");

    const [form, setForm] = useState({
        firstName: "",
        lastName: "",
        userName: "",
        email: "",
        password: "",
        confirmPassword: ""
    });

    const handleChange = e => {

        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {

        e.preventDefault();

        setError("");

        if (form.password !== form.confirmPassword) {

            setError("Passwords do not match.");

            return;
        }

        try {

            await register(form);

            navigate("/login");

        } catch (err) {

            setError(
                err.response?.data?.message ??
                "Registration failed."
            );

        }

    };

    return (

        <div className="row justify-content-center">

            <div className="col-md-6">

                <div className="card shadow">

                    <div className="card-body">

                        <h3 className="text-center">

                            Register

                        </h3>

                        {
                            error &&
                            <div className="alert alert-danger">

                                {error}

                            </div>
                        }

                        <form onSubmit={handleSubmit}>

                            <input
                                className="form-control mb-3"
                                placeholder="First Name"
                                name="firstName"
                                onChange={handleChange}
                                required
                            />

                            <input
                                className="form-control mb-3"
                                placeholder="Last Name"
                                name="lastName"
                                onChange={handleChange}
                                required
                            />

                            <input
                                className="form-control mb-3"
                                placeholder="Username"
                                name="userName"
                                onChange={handleChange}
                                required
                            />

                            <input
                                type="email"
                                className="form-control mb-3"
                                placeholder="Email"
                                name="email"
                                onChange={handleChange}
                                required
                            />

                            <input
                                type="password"
                                className="form-control mb-3"
                                placeholder="Password"
                                name="password"
                                onChange={handleChange}
                                required
                            />

                            <input
                                type="password"
                                className="form-control mb-3"
                                placeholder="Confirm Password"
                                name="confirmPassword"
                                onChange={handleChange}
                                required
                            />

                            <button className="btn btn-success w-100">

                                Register

                            </button>

                        </form>

                        <hr />

                        <div className="text-center">

                            <Link to="/login">

                                Back to Login

                            </Link>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );
}

export default Register;