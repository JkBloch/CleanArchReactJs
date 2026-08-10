import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {FaArrowLeft,FaMinusCircle,FaEdit,FaUndo,FaRegTimesCircle} from "react-icons/fa";
import {deleteUser,deletePermanentUsers,getUser,restoreUser} from "../../../api/admin/userApi";import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { getErrorMessage } from "../../../utils/errorHandling";

function UserDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [user, setUser] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);

    const [loading, setLoading] = useState(false);

    useEffect(() => {

        loadUser();

    }, []);

    async function loadUser() {

        const response = await getUser(id);

        setUser(response.data.data);

    }

    async function removeUser() {

        setLoading(true);

        try {

            await deleteUser(id);
            notify.success(

                "User deleted successfully."

            );
            await loadUser();
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
    async function removePermanentUser() {

        setLoading(true);

        try {

            await deletePermanentUsers(id);
            notify.success(

                "User Permanent deleted successfully."

            );
            navigate("/users");

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

        await restoreUser(id);

        notify.success(

            "User restore successfully."

        );
        await loadUser();
        setShowRestore(false);
    }

    if (!user)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        User Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>FirstName</strong>

                                <br />

                                {user.firstName}

                            </p>
                            </div>
                        <div className="col-md-6">

                            <p>

                                <strong>LastName</strong>

                                <br />

                                {user.lastName}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>LastName</strong>

                                <br />

                                {user.lastName}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>UserName</strong>

                                <br />

                                {user.userName}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Email</strong>

                                <br />

                                {user.email}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>PhoneNumber</strong>

                                <br />

                                {user.phoneNumber}

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>AccessFailedCount</strong>

                                <br />

                                {user.accessFailedCount}

                            </p>

                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Delete Status</strong>

                                <br />

                                {
                                    user.isDeleted
                                        ? "Deleted"
                                        : "Active"
                                }

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Active Status</strong>

                                <br />

                                {
                                    user.isActive
                                        ? "Actived"
                                        : "InActive"
                                }

                            </p>
                        </div>
                        <div className="col-md-6">

                            <p>

                                <strong>Locking Status</strong>

                                <br />

                                {
                                    user.isLocked
                                        ? "Locked"
                                        : "UnLocked"
                                }

                            </p>

                        </div>

                    </div>

                    <hr />
                    <div>
                        <button
                            className="icon-btn-warning icon-btn no-underline"
                            onClick={() => navigate(`/users/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            user.isDeleted

                                ?

                                <button
                                    className="icon-btn-success icon-btn"
                                    onClick={() => setShowRestore(true) } >
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
                        <Link to="/users"
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
                title="Delete User"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeUser}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDelete(false)}

            />
            <ConfirmDialog
                show={showDeletePermanent}
                title="Permenent Delete User"
                message="Are your sure want to delete record permenent ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removePermanentUser}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore User"
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

export default UserDetails;