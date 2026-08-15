import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { FaArrowLeft, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import { deletePermission, deletePermanentPermissions, getPermission, restorePermission } from "../../../api/admin/permissionApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
//import { getErrorMessage } from "../../../utils/errorHandling";

function PermissionDetails() {

    const { id } = useParams();
    const navigate = useNavigate();
    const [permission, setPermission] = useState(null);
    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);
    useEffect(() => {

        loadPermission();

    }, []);

    async function loadPermission() {
        const response = await getPermission(id);
        setPermission(response.data.data);
    }

    async function removePermission() {
        setLoading(true);
        try {
            await deletePermission(id);
            notify.success("Permission deleted successfully.");
            loadPermission();
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
    async function removePermanentPermission() {
        setLoading(true);
        try {
            await deletePermanentPermissions(id);
            notify.success("Permission Permanent deleted successfully.");
            navigate("/permissions");
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
        await restorePermission(id);
        notify.success("Permission restore successfully.");
        loadPermission();
        setShowRestore(false);
    }

    if (!permission)
        return <div>Loading...</div>;

    return (
        <div className="container">
            <div className="card shadow">
                <div className="card-header">
                    <h3>
                        Permission Details
                    </h3>
                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>Code</strong>

                                <br />

                                {permission.code}

                            </p>

                            <p>

                                <strong>Name</strong>

                                <br />

                                {permission.name}

                            </p>



                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    permission.isDeleted
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
                            onClick={() => navigate(`/permissions/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            permission.isDeleted

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
                        <Link to="/permissions"
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
                title="Delete Permission"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermission}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDelete(false)}

            />
            <ConfirmDialog
                show={showDeletePermanent}
                title="Permenent Delete Permission"
                message="Are your sure want to delete record permenent ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermanentPermission}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore Permission"
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

export default PermissionDetails;