import { useState, useEffect } from "react";

function EmployeeForm({
    initialValues,
    onSubmit,
    loading
}) {

    const [employee, setEmployee] = useState(initialValues);

    useEffect(() => {
        setEmployee(initialValues);
    }, [initialValues]);

    function handleChange(e) {

        const { name, value } = e.target;

        setEmployee(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(employee);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">

                <div className="col-md-6 mb-3">

                    <label>Employee Code</label>

                    <input
                        className="form-control"
                        name="employeeCode"
                        value={employee.employeeCode}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Department</label>

                    <input
                        className="form-control"
                        name="department"
                        value={employee.department}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>First Name</label>

                    <input
                        className="form-control"
                        name="firstName"
                        value={employee.firstName}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Last Name</label>

                    <input
                        className="form-control"
                        name="lastName"
                        value={employee.lastName}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Email</label>

                    <input
                        type="email"
                        className="form-control"
                        name="email"
                        value={employee.email}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Phone Number</label>

                    <input
                        className="form-control"
                        name="phoneNumber"
                        value={employee.phoneNumber}
                        onChange={handleChange}
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Salary</label>

                    <input
                        type="number"
                        className="form-control"
                        name="salary"
                        value={employee.salary}
                        onChange={handleChange}
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Joining Date</label>

                    <input
                        type="date"
                        className="form-control"
                        name="joiningDate"
                        value={employee.joiningDate}
                        onChange={handleChange}
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Date of Birth</label>

                    <input
                        type="date"
                        className="form-control"
                        name="dateOfBirth"
                        value={employee.dateOfBirth}
                        onChange={handleChange}
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Gender</label>

                    <select
                        className="form-select"
                        name="gender"
                        value={employee.gender}
                        onChange={handleChange}
                    >
                        <option value="1">Male</option>
                        <option value="2">Female</option>
                        <option value="3">Other</option>
                    </select>

                </div>

            </div>

            <button
                className="btn btn-success"
                disabled={loading}
            >
                Save Employee
            </button>

        </form>

    );
}

export default EmployeeForm;