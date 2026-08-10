import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteUserRole, restoreUserRole, searchUserRoles, deletePermanentUserRoles } from "../../../api/admin/userRoleApi";
import Loader from "../../../components/common/Loader";
import UserRoleTable from "../../../components/admin/userRole/UserRoleTable";
import { FaPlus } from "react-icons/fa";
import UserRoleSearch from "./UserRoleSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import { EMPTY_GUID } from "../../../constants/common";

function UserRoleList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [userRoles, setUserRoles] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchRoleId, setSearchRoleId] = useState(EMPTY_GUID);
    const [searchUserId, setSearchUserId] = useState(EMPTY_GUID);
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("role");
    const [descending, setDescending] = useState(false);

    const [filters, setFilters] = useState({

        keyword: "",
        roleId: EMPTY_GUID,
        userId: EMPTY_GUID,
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
            case "userId":
                setSearchUserId(value);
                break;
            default:
        }
        setFilters(prev => ({
            ...prev,
            [name]: value,
            pageNumber: 1
        }));

    }
    async function loadUserRoles(selectedPageNumber = 1, sortBy = "role", descending = false) {

        try {

            const revfilters = {
                keyword: keyword,
                roleId: searchRoleId,
                userId: searchUserId,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchUserRoles(revfilters);

            const result = response.data.data;

            setUserRoles(result.items);

            setTotalPages(result.totalPages);

        } finally {

            setLoading(false);

        }
    }

    useEffect(() => {

        loadUserRoles();

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
        await deleteUserRole(selectedId);
        notify.success("UserRole deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentUserRoles(selectedId);
            notify.success("UserRole Permanent deleted successfully.");
            setShowConfirmDeletePermanent(false);
        }
        catch (error) {
            notify.error(getErrorMessage(error));
            setShowConfirmDeletePermanent(false);
        }
        finally {
            setShowConfirmDeletePermanent(false);
        }

    };

    const handleConfirmRestore = async () => {
        await restoreUserRole(selectedId);
        notify.success("UserRole resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    UserRoles
                </h2>
                <div>
                    <NavLink to="/userRoles/create"
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
            <UserRoleSearch
                handleChange={handleChange}
                filters={filters}
                loadUserRoles={loadUserRoles}
                loading={loading}
                setLoading={setLoading}
                keyword={keyword}
                searchRoleId={searchRoleId}
                searchUserId={searchUserId}
                selectedPageNumber={pageNumber}
                pageSize={pageSize}
                sortBy={sortBy}
                descending={descending}
            />
            <UserRoleTable
                userRoles={userRoles}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadUserRoles}
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
                loadData={loadUserRoles}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete UserRole"
                message="Are you sure you want to delete this userRole?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadUserRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete UserRole Permanent"
                message="Are you sure you want to delete Permanent this userRole?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadUserRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore UserRole"
                message="Are you sure you want to restore this userRole?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadUserRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default UserRoleList;