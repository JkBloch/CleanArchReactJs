import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { FaArrowLeft, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import { deleteUserRole, deletePermanentUserRoles, getUserRole, restoreUserRole } from "../../../api/admin/userRoleApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
//import { getErrorMessage } from "../../../utils/errorHandling";

function UserRoleDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [userRole, setUserRole] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);

    useEffect(() => {

        loadUserRole();

    }, []);

    async function loadUserRole() {

        const response = await getUserRole(id);

        setUserRole(response.data.data);

    }

    async function removeUserRole() {

        setLoading(true);

        try {

            await deleteUserRole(id);
            notify.success(

                "UserRole deleted successfully."

            );
            await loadUserRole();
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
    async function removePermanentUserRole() {

        setLoading(true);

        try {

            await deletePermanentUserRoles(id);
            notify.success(

                "UserRole Permanent deleted successfully."

            );
            navigate("/userRoles");

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

        await restoreUserRole(id);

        notify.success(

            "UserRole restore successfully."

        );
        await loadUserRole();
        setShowRestore(false);
    }

    if (!userRole)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        UserRole Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            

                            <p>

                                <strong>User</strong>

                                <br />

                                {userRole.userName}

                            </p>

                            <p>

                                <strong>Role</strong>

                                <br />

                                {userRole.roleName}

                            </p>

                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    userRole.isDeleted
                                        ? "Deleted"
                                        : "Active"
                                }

                            </p>

                        </div>

                    </div>

                    <hr />
                    <div>
                        <button
                            className="icon-btn-warning icon-btn no-underline"
                            onClick={() => navigate(`/userRoles/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            userRole.isDeleted

                                ?

                                <button
                                    className="icon-btn-success icon-btn"
                                    onClick={() => setShowRestore(true)
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
                        <Link to="/userRoles"
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
                title="Delete UserRole"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeUserRole}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDelete(false)}

            />
            <ConfirmDialog
                show={showDeletePermanent}
                title="Permenent Delete UserRole"
                message="Are your sure want to delete record permenent ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermanentUserRole}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore UserRole"
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

export default UserRoleDetails;