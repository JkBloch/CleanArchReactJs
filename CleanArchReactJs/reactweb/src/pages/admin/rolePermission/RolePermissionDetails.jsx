import {useEffect,useState} from "react";
import {useNavigate,useParams} from "react-router-dom";
import {FaArrowLeft,FaMinusCircle,FaEdit,FaUndo,FaRegTimesCircle} from "react-icons/fa";
import {deleteRolePermission,deletePermanentRolePermissions,getRolePermission,restoreRolePermission} from "../../../api/admin/rolePermissionApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { getErrorMessage } from "../../../utils/errorHandling";

function RolePermissionDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [rolePermission, setRolePermission] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);

    useEffect(() => {

        loadRolePermission();

    }, []);

    async function loadRolePermission() {

        const response = await getRolePermission(id);

        setRolePermission(response.data.data);

    }

    async function removeRolePermission() {

        setLoading(true);

        try {

            await deleteRolePermission(id);
            notify.success(

                "RolePermission deleted successfully."

            );
            await loadRolePermission();
            setShowDelete(false); 

        }
        catch (error) {
            notify.error(getErrorMessage(error));
            setShowDelete(false);
        }
        finally {

            setLoading(false);

        }

    }
    async function removePermanentRolePermission() {

        setLoading(true);

        try {

            await deletePermanentRolePermissions(id);
            notify.success(

                "RolePermission Permanent deleted successfully."

            );
            navigate("/rolePermissions");

        }
        catch (error) {
            notify.error(getErrorMessage(error));
            setShowDeletePermanent(false);
        }
        finally {

            setLoading(false);

        }

    }

    async function restore() {

        await restoreRolePermission(id);

        notify.success(

            "RolePermission restore successfully."

        );
        await loadRolePermission();
        setShowRestore(false);
    }

    if (!rolePermission)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        RolePermission Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>Role</strong>

                                <br />

                                {rolePermission.roleName}

                            </p>

                            <p>

                                <strong>Permission</strong>

                                <br />

                                {rolePermission.permissionName}

                            </p>



                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    rolePermission.isDeleted
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
                            onClick={() => navigate(`/rolePermissions/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            rolePermission.isDeleted

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
                        <Link to="/rolePermissions"
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
                title="Delete RolePermission"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeRolePermission}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDelete(false)}

            />
            <ConfirmDialog
                show={showDeletePermanent}
                title="Permenent Delete RolePermission"
                message="Are your sure want to delete record permenent ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermanentRolePermission}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore RolePermission"
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

export default RolePermissionDetails;