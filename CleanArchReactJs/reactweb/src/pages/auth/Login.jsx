import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import Loader from "../../components/common/Loader";

function Login() {

    const navigate = useNavigate();

    const { login } = useAuth();

    const [loading, setLoading] = useState(false);

    const [error, setError] = useState("");

    const [showPassword, setShowPassword] = useState(false);

    const [form, setForm] = useState({
        userNameOrEmail: "",
        password: ""
    });

    const handleChange = e => {

        setForm({
            ...form,
            [e.target.name]: e.target.value
        });
    };

    const handleSubmit = async e => {

        e.preventDefault();

        setLoading(true);

        setError("");

        try {

            await login(form);

            navigate("/", {
                replace: true
            });

        } catch (err) {

            setError(
                err.response?.data?.message ??
                "Login failed."
            );

        } finally {

            setLoading(false);

        }
    };

    return (

        <div className="row justify-content-center">

            <div className="col-md-5">

                <div className="card shadow">

                    <div className="card-body">

                        <h3 className="text-center mb-4">

                            Employee Login

                        </h3>

                        {
                            error &&
                            <div className="alert alert-danger">

                                {error}

                            </div>
                        }

                        <form onSubmit={handleSubmit}>

                            <div className="mb-3">

                                <label>

                                    Username / Email

                                </label>

                                <input
                                    type="text"
                                    name="userNameOrEmail"
                                    className="form-control"
                                    value={form.userNameOrEmail}
                                    onChange={handleChange}
                                    required
                                />

                            </div>

                            <div className="mb-3">

                                <label>Password</label>

                                <div className="input-group">

                                    <input
                                        type={
                                            showPassword
                                                ? "text"
                                                : "password"
                                        }
                                        name="password"
                                        className="form-control"
                                        value={form.password}
                                        onChange={handleChange}
                                        required
                                    />

                                    <button
                                        type="button"
                                        className="btn btn-outline-secondary"
                                        onClick={() =>
                                            setShowPassword(!showPassword)
                                        }
                                    >
                                        {showPassword ? "Hide" : "Show"}
                                    </button>

                                </div>

                            </div>

                            <button
                                className="btn btn-primary w-100"
                                disabled={loading}
                            >
                                Login
                            </button>

                        </form>

                        {loading && <Loader />}

                        <hr />

                        <div className="text-center">

                            <Link to="/register">

                                Create Account

                            </Link>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );
}

export default Login;