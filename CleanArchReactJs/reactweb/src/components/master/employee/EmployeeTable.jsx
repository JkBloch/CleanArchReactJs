import { Link } from "react-router-dom";
import { FaRegEye, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import EmployeePhoto from "../../../components/master/employee/EmployeePhoto";
import  fromdate  from "../../../utils/dateUtils";
function EmployeeTable({
    employees,
    onDelete,
    onDeletePermanent,
    onRestore,
    loadData,
    pageNumber,
    sortBy,
    setSortBy,
    descending,
    setDescending

}) {
    const onSortData = (column) => {
        var des = descending;
        if (column === sortBy) {
            setDescending(!descending);
            des = !descending;
        }
        setSortBy(column);
        loadData(pageNumber, column, des)
    }
    return (

        <table className="table table-striped table-hover">

            <thead className="table-dark">

                <tr>
                    <th style={{ cursor: "pointer" }} >
                        Photo
                    </th>

                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("code")} >
                        Code
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("name")} >
                        Name
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("email")} >
                        Email
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("department")} >
                        Department
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("state")} >
                        State
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("city")} >
                        City
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("salary")} >
                        Salary
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("dateOfBirth")} >
                        DateOfBirth
                    </th>
                    <th style={{ cursor: "pointer" }}
                        onClick={() => onSortData("joiningDate")} >
                        JoiningDate
                    </th>                  

                    <th>

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    employees.map(employee => (

                        <tr key={employee.id}>
                            <td>
                                <EmployeePhoto
                                    photoUrl={employee.photoUrl}
                                    firstName={employee.name}
                                    lastName={employee.name}
                                    size={50}
                                />
                            </td>


                            <td>
                                {employee.code}
                            </td>

                            <td>
                                {employee.name}
                            </td>
                            <td>
                                {employee.email}
                            </td>
                            <td>
                                {employee.departmentName}
                            </td>
                            <td>
                                {employee.stateName}
                            </td>
                            <td>
                                {employee.cityName}
                            </td>
                            <td>
                                {employee.salary}
                            </td>
                            <td>
                                {fromdate(employee.dateOfBirth)}
                            </td>
                            <td>
                                {fromdate(employee.joiningDate)}
                            </td> 

                            <td>
                                <div>
                                    <Link to={`/employees/${employee.id}`}
                                        className="icon-btn-info icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaRegEye></FaRegEye>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            View
                                        </span>
                                    </Link>

                                    <Link to={`/employees/edit/${employee.id}`}
                                        className="icon-btn-warning icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaEdit></FaEdit>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Edit
                                        </span>
                                    </Link>
                                    {

                                        employee.isDeleted
                                            ?
                                            <button className="icon-btn-success icon-btn"
                                                onClick={() => onRestore(employee.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaUndo ></FaUndo>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Restore
                                                </span>
                                            </button>
                                            :

                                            <button className="icon-btn-danger icon-btn"
                                                onClick={() => onDelete(employee.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaMinusCircle ></FaMinusCircle>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Delete
                                                </span>
                                            </button>

                                    }  
                                </div>
                            </td>

                        </tr>

                    ))
                }

            </tbody>

        </table>

    );
}

export default EmployeeTable;