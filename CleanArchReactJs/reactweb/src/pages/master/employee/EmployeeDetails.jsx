import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { FaArrowLeft, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import { deleteEmployee, deletePermanentEmployees, getEmployee, restoreEmployee } from "../../../api/master/employeeApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
//import { getErrorMessage } from "../../../utils/errorHandling";

function EmployeeDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [employee, setEmployee] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadEmployee();

    }, []);

    async function loadEmployee() {
        const response = await getEmployee(id);
        setEmployee(response.data.data);
    }

    async function removeEmployee() {
        setLoading(true);
        try {
            await deleteEmployee(id);
            notify.success("Employee deleted successfully.");
            loadEmployee();
            setShowDelete(false);
        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setShowDelete(false);
        }
        finally {
            setLoading(false);
        }
    }
    async function removePermanentEmployee() {
        setLoading(true);
        try {

            await deletePermanentEmployees(id);
            notify.success("Employee Permanent deleted successfully.");
            navigate("/employees");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setShowDeletePermanent(false);
        }
        finally {

            setLoading(false);

        }

    }

    async function restore() {

        await restoreEmployee(id);

        notify.success(

            "Employee restore successfully."

        );
        loadEmployee();
        setShowRestore(false);
    }

    if (!employee)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        Employee Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>Code</strong>

                                <br />

                                {employee.code}

                            </p>
                            </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Name</strong>

                                <br />

                                {employee.name}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Email</strong>

                                <br />

                                {employee.email}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>PhoneNumber</strong>

                                <br />

                                {employee.phoneNumber}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Department</strong>

                                <br />

                                {employee.departmentName}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>State</strong>

                                <br />

                                {employee.stateName}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>City</strong>

                                <br />

                                {employee.cityName}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Salary</strong>

                                <br />

                                {employee.salary}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>DateOfBirth</strong>

                                <br />

                                {employee.dateOfBirth}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>JoiningDate</strong>

                                <br />

                                {employee.joiningDate}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Gender</strong>

                                <br />

                                {
                                    (employee.gender == 0) ? "Select" :
                                        (employee.gender == 1) ? "Male" :
                                            (employee.gender == 2) ? "FeMale" :
                                                (employee.gender == 2) ? "Other" :
                                        "Select"
                                


                                    }

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>IsActive</strong>

                                <br />
                                {
                                    employee.isActive
                                        ? "Active"
                                        : "InActive"
                                }


                            </p>

                        </div>
                        <div className="col-md-6">


                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    employee.isDeleted
                                        ? "Deleted"
                                        : "Not Deleted"
                                }

                            </p>

                        </div>

                    </div>

                    <hr />
                    <div>
                        <button
                            className="icon-btn-warning icon-btn no-underline"
                            onClick={() => navigate(`/employees/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            employee.isDeleted

                                ?

                                <button
                                    className="icon-btn-success icon-btn"
                                    onClick={() =>
                                        setShowRestore(true)
                                    }
                                >
                                    <span className="icon-section">
                                        <FaUndo></FaUndo>
                                    </span>
                                    <span className="text-section">
                                        Restore
                                    </span>


                                </button>

                                :

                                <button
                                    className="icon-btn-danger icon-btn"
                                    onClick={() =>
                                        setShowDelete(true)
                                    }
                                >
                                    <span className="icon-section">
                                        <FaMinusCircle></FaMinusCircle>
                                    </span>
                                    <span className="text-section">
                                        Delete
                                    </span>

                                </button>

                        }
                        <button
                            className="icon-btn-danger icon-btn"
                            onClick={() =>
                                setShowDeletePermanent(true)
                            }
                        >
                            <span className="icon-section">
                                <FaRegTimesCircle></FaRegTimesCircle>
                            </span>
                            <span className="text-section">
                                Delete Permanent
                            </span>


                        </button>
                        <Link to="/employees"
                            className="icon-btn-info icon-btn no-underline">

                            <span className="icon-section">
                                <FaArrowLeft></FaArrowLeft>
                            </span>
                            <span className="text-section">
                                Go Back
                            </span>

                        </Link>
                    </div>
                </div>

            </div>
            <ConfirmDialog
                show={showDelete}
                title="Delete Employee"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeEmployee}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDelete(false)}

            />
            <ConfirmDialog
                show={showDeletePermanent}
                title="Permanent Delete Permission"
                message="Are your sure want to delete record permanent ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermanentEmployee}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore Employee"
                message="Are your sure want to restore record ?"
                confirmText="Restore"
                cancelText="Cancel"
                confirmVariant="success"
                onConfirm={restore}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowRestore(false)}
            />
        </div>

    );

}

export default EmployeeDetails;