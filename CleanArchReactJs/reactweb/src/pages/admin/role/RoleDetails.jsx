import {

    useEffect,
    useState

} from "react";

import {

    useNavigate,
    useParams

} from "react-router-dom";
import {
    FaArrowLeft,
    FaMinusCircle,
    FaEdit,
    FaUndo,
    FaRegTimesCircle
} from "react-icons/fa";
import {

    deleteRole,
    deletePermanentRoles,
    getRole,
    restoreRole

} from "../../../api/roleApi";

import RoleDeleteModal from "./RoleDeleteModal";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";

function RoleDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [role, setRole] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);

    const [loading, setLoading] = useState(false);

    useEffect(() => {

        loadRole();

    }, []);

    async function loadRole() {

        const response = await getRole(id);

        setRole(response.data.data);

    }

    async function removeRole() {

        setLoading(true);

        try {

            await deleteRole(id);
            notify.success(

                "Role deleted successfully."

            );
            navigate("/roles");

        }
        finally {

            setLoading(false);

        }

    }
    async function removePermanentRole() {

        setLoading(true);

        try {

            await deletePermanentRoles(id);
            notify.success(

                "Role Permanent deleted successfully."

            );
            navigate("/roles");

        }
        finally {

            setLoading(false);

        }

    }

    async function restore() {

        await restoreRole(id);

        notify.success(

            "Role restore successfully."

        );
        navigate("/roles");

    }

    if (!role)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        Role Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">

                            <p>

                                <strong>Code</strong>

                                <br />

                                {role.code}

                            </p>

                            <p>

                                <strong>Name</strong>

                                <br />

                                {role.name}

                            </p>



                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    role.isDeleted
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
                            onClick={() => navigate(`/roles/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            role.isDeleted

                                ?

                                <button
                                    className="icon-btn-success icon-btn"
                                    onClick={restore}
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
                        <Link to="/roles"
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

            <RoleDeleteModal

                show={showDelete}

                role={role}

                loading={loading}

                onDelete={removeRole}

                onCancel={() =>
                    setShowDelete(false)
                }

            />
            <RoleDeleteModal

                show={showDeletePermanent}

                role={role}

                loading={loading}

                onDelete={removePermanentRole}

                onCancel={() =>
                    setShowDeletePermanent(false)
                }

            />
        </div>

    );

}

export default RoleDetails;