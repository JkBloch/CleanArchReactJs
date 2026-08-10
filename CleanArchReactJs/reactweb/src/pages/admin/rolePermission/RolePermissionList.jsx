import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import {  deleteRolePermission, restoreRolePermission, searchRolePermissions, deletePermanentRolePermissions } from "../../../api/admin/rolePermissionApi";
import Loader from "../../../components/common/Loader";
import RolePermissionTable from "../../../components/admin/rolePermission/RolePermissionTable";
import { FaPlus } from "react-icons/fa";
import RolePermissionSearch from "../rolePermission/RolePermissionSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import { EMPTY_GUID } from "../../../constants/common";

function RolePermissionList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [rolePermissions, setRolePermissions] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchRoleId, setSearchRoleId] = useState(EMPTY_GUID);
    const [searchPermissionId, setSearchPermissionId] = useState(EMPTY_GUID);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("role");
    const [descending, setDescending] = useState(false);

    const [filters, setFilters] = useState({

        keyword: "",
        roleId: EMPTY_GUID,
        permissionId: EMPTY_GUID,
        pageNumber: 1,
        pageSize: 10,
        sortBy: sortBy,
        descending: false
    });
    const [loading, setLoading] = useState(true);
    function handleChange(e) {

        const { name, value } = e.target;
        switch (name) {
            case "keyword":
                setKeyword(value);
                break;
            case "roleId":
                setSearchRoleId(value);
                break;
            case "permissionId":
                setSearchPermissionId(value);
                break;
            default:
        }
        setFilters(prev => ({
            ...prev,
            [name]: value,
            pageNumber: 1
        }));

    }
    async function loadRolePermissions(selectedPageNumber = 1, sortBy = "role", descending = false) {

        try {

            //const response = await getRolePermissions();
            //setRolePermissions(response.data.data ?? []);
            const revfilters = {
                keyword: keyword,
                roleId: searchRoleId,
                permissionId: searchPermissionId,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchRolePermissions(revfilters);

            const result = response.data.data;

            setRolePermissions(result.items);

            setTotalPages(result.totalPages);

        } finally {

            setLoading(false);

        }
    }

    useEffect(() => {

        loadRolePermissions();

    }, []);

    const handleDelete = (id) => {
        setSelectedId(id);
        setShowConfirmDelete(true);
    };
    const handleDeletePermanent = (id) => {
        setSelectedId(id);
        setShowConfirmDeletePermanent(true);
    };
    const handleRestore = (id) => {
        setSelectedId(id);
        setShowConfirmRestore(true);
    };

    const handleConfirmDelete = async () => {
        await deleteRolePermission(selectedId);
        notify.success("RolePermission deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentRolePermissions(selectedId);
            notify.success("RolePermission Permanent deleted successfully.");
            setShowConfirmDeletePermanent(false);
        } 
        catch(error) {
            notify.error(getErrorMessage(error));
            setShowConfirmDeletePermanent(false);
        }
        finally {
            setShowConfirmDeletePermanent(false);
        }
        
    };

    const handleConfirmRestore = async () => {
        await restoreRolePermission(selectedId);
        notify.success("RolePermission resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    RolePermissions
                </h2>
                <div>
                    <NavLink to="/rolePermissions/create"
                        className="icon-btn icon-btn-success no-underline m-1">
                        <span className="icon-section">
                            <FaPlus ></FaPlus>
                        </span>
                        <span className="text-section">
                            Add
                        </span>
                    </NavLink>

                </div>
            </div>
            <RolePermissionSearch
                handleChange={handleChange}
                filters={filters}
                loadRolePermissions={loadRolePermissions}
                loading={loading}
                setLoading={setLoading}
                keyword={keyword}
                searchRoleId={searchRoleId}
                searchPermissionId={searchPermissionId}
                selectedPageNumber={pageNumber}
                pageSize={pageSize}
                sortBy={sortBy}
                descending={descending}
            />
            <RolePermissionTable
                rolePermissions={rolePermissions}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadRolePermissions}
                pageNumber={pageNumber}
                sortBy={sortBy}
                setSortBy={setSortBy}
                descending={descending}
                setDescending={setDescending}

            />
            <Pagination
                pageNumber={pageNumber}
                totalPages={totalPages}
                setPageNumber={setPageNumber}
                loadData={loadRolePermissions}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete RolePermission"
                message="Are you sure you want to delete this rolePermission?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadRolePermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete RolePermission Permanent"
                message="Are you sure you want to delete Permanent this rolePermission?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadRolePermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore RolePermission"
                message="Are you sure you want to restore this rolePermission?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadRolePermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default RolePermissionList;