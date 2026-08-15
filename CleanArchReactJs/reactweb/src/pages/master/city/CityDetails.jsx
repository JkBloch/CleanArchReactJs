import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { FaArrowLeft, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
import { deleteCity, deletePermanentCities, getCity, restoreCity } from "../../../api/master/cityApi";
import { notify } from "../../../services/notificationService";
import { Link } from "react-router-dom";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
//import { getErrorMessage } from "../../../utils/errorHandling";

function CityDetails() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [city, setCity] = useState(null);

    const [showDelete, setShowDelete] = useState(false);
    const [showDeletePermanent, setShowDeletePermanent] = useState(false);
    const [showRestore, setShowRestore] = useState(false);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        loadCity();

    }, []);

    async function loadCity() {
        const response = await getCity(id);
        setCity(response.data.data);
    }

    async function removeCity() {
        setLoading(true);
        try {
            await deleteCity(id);
            notify.success("City deleted successfully.");
            loadCity();
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
    async function removePermanentCity() {
        setLoading(true);
        try {

            await deletePermanentCities(id);
            notify.success("City Permanent deleted successfully.");
            navigate("/cities");

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

        await restoreCity(id);

        notify.success(

            "City restore successfully."

        );
        loadCity();
        setShowRestore(false);
    }

    if (!city)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <div className="card shadow">

                <div className="card-header">

                    <h3>

                        City Details

                    </h3>

                </div>

                <div className="card-body">

                    <div className="row">

                        <div className="col-md-6">
                            <p>

                                <strong>State</strong>

                                <br />

                                {city.stateName}

                            </p>

                            <p>

                                <strong>Code</strong>

                                <br />

                                {city.code}

                            </p>

                            <p>

                                <strong>Name</strong>

                                <br />

                                {city.name}

                            </p>



                            <p>

                                <strong>Status</strong>

                                <br />

                                {
                                    city.isDeleted
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
                            onClick={() => navigate(`/cities/edit/${id}`)} >
                            <span className="icon-section">
                                <FaEdit></FaEdit>
                            </span>
                            <span className="text-section">
                                Edit
                            </span>

                        </button>

                        {

                            city.isDeleted

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
                        <Link to="/cities"
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
                title="Delete City"
                message="Are your sure wan't to delete record ?"
                confirmText="Delete"
                cancelText="Cancel"
                confirmVariant="danger"
                onConfirm={removeCity}
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
                onConfirm={removePermanentCity}
                loadData={undefined}
                pageNumber="1"
                onCancel={() => setShowDeletePermanent(false)}

            />
            <ConfirmDialog
                show={showRestore}
                title="Restore City"
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

export default CityDetails;