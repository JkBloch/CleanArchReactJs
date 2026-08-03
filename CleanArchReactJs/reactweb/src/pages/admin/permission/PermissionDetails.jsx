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

    deletePermission,
    deletePermanentPermissions,
    getPermission,
    restorePermission

} from "../../../api/permissionApi";

import PermissionDeleteModal from "./PermissionDeleteModal";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";

function PermissionDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [permission, setPermission] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);

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
            notify.success(

                "Permission deleted successfully."

            );
            navigate("/permissions");

        }
        finally {

            setLoading(false);

        }

    }
    async function removePermanentPermission() {

        setLoading(true);

        try {

            await deletePermanentPermissions(id);
            notify.success(

                "Permission Permanent deleted successfully."

            );
            navigate("/permissions");

        }
        finally {

            setLoading(false);

        }

    }

    async function restore() {

        await restorePermission(id);

        notify.success(

            "Permission restore successfully."

        );
        navigate("/permissions");

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

            <PermissionDeleteModal

                show={showDelete}

                permission={permission}

                loading={loading}

                onDelete={removePermission}

                onCancel={() =>
                    setShowDelete(false)
                }

            />
            <PermissionDeleteModal

                show={showDeletePermanent}

                permission={permission}

                loading={loading}

                onDelete={removePermanentPermission}

                onCancel={() =>
                    setShowDeletePermanent(false)
                }

            />
        </div>

    );

}

export default PermissionDetails;