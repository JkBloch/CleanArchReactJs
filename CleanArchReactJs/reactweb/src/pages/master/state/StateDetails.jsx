import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { FaArrowLeft, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import { deleteState, deletePermanentStates, getState, restoreState } from "../../../api/master/stateApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
//import { getErrorMessage } from "../../../utils/errorHandling";

function StateDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [state, setState] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadState();

    }, []);

    async function loadState() {
        const response = await getState(id);
        setState(response.data.data);
    }

    async function removeState() {
        setLoading(true);
        try {
            await deleteState(id);
            notify.success("State deleted successfully.");
            loadState();
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
    async function removePermanentState() {
        setLoading(true);
        try {

            await deletePermanentStates(id);
            notify.success("State Permanent deleted successfully.");
            navigate("/states");

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

        await restoreState(id);

        notify.success(

            "State restore successfully."

        );
        loadState();
        setShowRestore(false);
    }

    if (!state)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        State Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>Code</strong>

                                <br />

                                {state.code}

                            </p>

                            <p>

                                <strong>Name</strong>

                                <br />

                                {state.name}

                            </p>



                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    state.isDeleted
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
                            onClick={() => navigate(`/states/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            state.isDeleted

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
                        <Link to="/states"
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
                title="Delete State"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeState}
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
                onConfirm={removePermanentState}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore State"
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

export default StateDetails;