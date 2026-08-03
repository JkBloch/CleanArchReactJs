import { Link } from "react-router-dom";

function EmployeeTable({
    employees,
    onDelete,
    onSort
}) {

    return (

        <table className="table table-striped table-hover">

            <thead className="table-dark">

                <tr>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSort("employeeCode")}
                    >
                        Code
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSort("firstName")}
                    >
                        Name
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSort("email")}
                    >
                        Email
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSort("department")}
                    >
                        Department
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSort("salary")}
                    >
                        Salary
                    </th>

                    <th width="220">

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    employees.map(employee => (

                        <tr key={employee.id}>

                            <td>

                                {employee.employeeCode}

                            </td>

                            <td>

                                {employee.firstName} {employee.lastName}

                            </td>

                            <td>

                                {employee.email}

                            </td>

                            <td>

                                {employee.department}

                            </td>

                            <td>

                                ${employee.salary}

                            </td>

                            <td>

                                <Link
                                    to={`/employees/${employee.id}`}
                                    className="btn btn-info btn-sm me-2">

                                    View

                                </Link>

                                <Link
                                    to={`/employees/edit/${employee.id}`}
                                    className="btn btn-warning btn-sm me-2">

                                    Edit

                                </Link>

                                <button
                                    className="btn btn-danger btn-sm"
                                    onClick={() => onDelete(employee.id)}>

                                    Delete

                                </button>

                            </td>

                        </tr>

                    ))
                }

            </tbody>

        </table>

    );
}

export default EmployeeTable;