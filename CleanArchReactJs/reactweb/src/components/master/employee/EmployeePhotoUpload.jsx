import { useEffect, useRef, useState } from "react";
import EmployeePhoto from "./EmployeePhoto";
import {
    uploadEmployeePhoto,
    deleteEmployeePhoto
} from "../../../api/master/employeeApi";
import { getImageUrl } from "../../../utils/imageHelper";

function EmployeePhotoUpload({
    employee,
    onPhotoChanged
}) {

    const fileInputRef =
        useRef(null);

    const [selectedFile, setSelectedFile] =
        useState(null);

    const [previewUrl, setPreviewUrl] =
        useState(null);

    const [loading, setLoading] =
        useState(false);

    const [error, setError] =
        useState("");

    const [message, setMessage] =
        useState("");

    const MAX_FILE_SIZE =
        5 * 1024 * 1024;

    const ALLOWED_TYPES = [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];

    useEffect(() => {

        return () => {

            if (previewUrl) {
                URL.revokeObjectURL(previewUrl);
            }

        };

    }, [previewUrl]);

    const handleFileChange = event => {

        setError("");
        setMessage("");

        const file =
            event.target.files?.[0];

        if (!file)
            return;

        // Validate type
        if (!ALLOWED_TYPES.includes(file.type)) {

            setError(
                "Only JPG, PNG and WEBP images are allowed."
            );

            event.target.value = "";

            return;
        }

        // Validate size
        if (file.size > MAX_FILE_SIZE) {

            setError(
                "Maximum photo size is 5 MB."
            );

            event.target.value = "";

            return;
        }

        // Remove old preview URL
        if (previewUrl) {
            URL.revokeObjectURL(previewUrl);
        }

        setSelectedFile(file);

        setPreviewUrl(
            URL.createObjectURL(file)
        );
    };

    const handleUpload = async () => {

        if (!selectedFile) {

            setError(
                "Please select a photo first."
            );

            return;
        }

        try {

            setLoading(true);
            setError("");
            setMessage("");

            const response =
                await uploadEmployeePhoto(
                    employee.id,
                    selectedFile
                );

            setMessage(
                response.data?.message ||
                "Photo uploaded successfully."
            );

            setSelectedFile(null);

            if (previewUrl) {
                URL.revokeObjectURL(previewUrl);
            }

            setPreviewUrl(null);

            if (fileInputRef.current) {
                fileInputRef.current.value = "";
            }

            // Tell parent to reload/update employee
            if (onPhotoChanged) {
                await onPhotoChanged(
                    response.data?.data
                );
            }

        } catch (err) {

            console.error(
                "Photo upload error:",
                err
            );

            setError(
                err.response?.data?.message ||
                "Unable to upload employee photo."
            );

        } finally {

            setLoading(false);

        }
    };

    const handleDelete = async () => {

        const confirmed =
            window.confirm(
                "Are you sure you want to delete this photo?"
            );

        if (!confirmed)
            return;

        try {

            setLoading(true);
            setError("");
            setMessage("");

            const response =
                await deleteEmployeePhoto(
                    employee.id
                );

            setMessage(
                response.data?.message ||
                "Photo deleted successfully."
            );

            if (onPhotoChanged) {
                await onPhotoChanged(null);
            }

        } catch (err) {

            console.error(
                "Photo delete error:",
                err
            );

            setError(
                err.response?.data?.message ||
                "Unable to delete employee photo."
            );

        } finally {

            setLoading(false);

        }
    };

    const displayPhoto =
        previewUrl ||
        getImageUrl(employee.photoUrl);

    return (

        <div className="card shadow-sm">

            <div className="card-header">

                <h5 className="mb-0">
                    Employee Profile Photo
                </h5>

            </div>

            <div className="card-body">

                <div className="text-center">

                    {previewUrl ? (

                        <img
                            src={previewUrl}
                            alt="Preview"
                            className="
                                rounded-circle
                                border
                            "
                            style={{
                                width: 160,
                                height: 160,
                                objectFit: "cover"
                            }}
                        />

                    ) : (

                        <EmployeePhoto
                            photoUrl={
                                employee.photoUrl
                            }
                            firstName={
                                employee.firstName
                            }
                            lastName={
                                employee.lastName
                            }
                            size={160}
                        />

                    )}

                </div>

                {previewUrl && (

                    <div className="text-center mt-2">

                        <small className="text-muted">

                            New photo preview

                        </small>

                    </div>

                )}

                <div className="mt-4">

                    <label
                        className="form-label"
                        htmlFor="employeePhoto"
                    >
                        Select Photo
                    </label>

                    <input
                        ref={fileInputRef}
                        id="employeePhoto"
                        type="file"
                        className="form-control"
                        accept="image/jpeg,image/png,image/webp"
                        onChange={handleFileChange}
                        disabled={loading}
                    />

                    <div className="form-text">

                        JPG, PNG or WEBP. Maximum 5 MB.

                    </div>

                </div>

                {selectedFile && (

                    <div className="mt-3">

                        <div className="alert alert-info">

                            <strong>
                                Selected:
                            </strong>{" "}

                            {selectedFile.name}

                            <br />

                            <strong>
                                Size:
                            </strong>{" "}

                            {(
                                selectedFile.size /
                                1024 /
                                1024
                            ).toFixed(2)} MB

                        </div>

                    </div>

                )}

                {error && (

                    <div className="alert alert-danger mt-3">

                        {error}

                    </div>

                )}

                {message && (

                    <div className="alert alert-success mt-3">

                        {message}

                    </div>

                )}

                <div className="d-flex gap-2 mt-3">

                    {selectedFile && (

                        <button
                            type="button"
                            className="btn btn-primary"
                            onClick={handleUpload}
                            disabled={loading}
                        >

                            {loading ? (
                                <>
                                    <span
                                        className="
                                            spinner-border
                                            spinner-border-sm
                                            me-2
                                        "
                                    />

                                    Uploading...
                                </>
                            ) : (
                                "Upload Photo"
                            )}

                        </button>

                    )}

                    {employee.photoUrl && (

                        <button
                            type="button"
                            className="btn btn-outline-danger"
                            onClick={handleDelete}
                            disabled={loading}
                        >
                            Delete Photo
                        </button>

                    )}

                </div>

            </div>

        </div>
    );
}

export default EmployeePhotoUpload;