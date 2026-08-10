import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteUser, restoreUser, searchUsers, deletePermanentUsers } from "../../../api/admin/userApi";
import Loader from "../../../components/common/Loader";
import UserTable from "../../../components/admin/user/UserTable";
import { FaPlus } from "react-icons/fa";
import UserSearch from "../user/UserSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";

function UserList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [users, setUsers] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchFirstName, setSearchFirstName] = useState("");
    const [searchLastName, setSearchLastName] = useState("");
    const [searchUserName, setSearchUserName] = useState("");
    const [searchEmail, setSearchEmail] = useState("");
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("code");
    const [descending, setDescending] = useState(false);

    const [filters, setFilters] = useState({

        keyword: "",
        firstName: "",
        lastName: "",
        userName: "",
        email: "",
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
            case "firstName":
                setSearchFirstName(value);
                break;
            case "lastName":
                setSearchLastName(value);
                break;
            case "userName":
                setSearchUserName(value);
                break;
            case "email":
                setSearchEmail(value);
                break;
            default:
        }
        setFilters(prev => ({
            ...prev,
            [name]: value,
            pageNumber: 1
        }));

    }
    async function loadUsers(selectedPageNumber = 1, sortBy = "userName", descending = false) {

        try {

            //const response = await getUsers();
            //setUsers(response.data.data ?? []);
            const revfilters = {
                keyword: keyword,
                firstName: searchFirstName,
                lastName: searchLastName,
                userName: searchUserName,
                email: searchEmail,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchUsers(revfilters);

            const result = response.data.data;

            setUsers(result.items);

            setTotalPages(result.totalPages);

        } finally {

            setLoading(false);

        }
    }

    useEffect(() => {

        loadUsers();

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
        await deleteUser(selectedId);
        notify.success("User deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentUsers(selectedId);
            notify.success("User Permanent deleted successfully.");
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
        await restoreUser(selectedId);
        notify.success("User resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    Users
                </h2>
                <div>
                    <NavLink to="/users/create"
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
            <UserSearch
                handleChange={handleChange}
                filters={filters}
                loadUsers={loadUsers}
                loading={loading}
                setLoading={setLoading}
                keyword={keyword}
                searchFirstName={searchFirstName}
                searchLastName={searchLastName}
                searchUserName={searchUserName}
                searchEmail={searchEmail}
                selectedPageNumber={pageNumber}
                pageSize={pageSize}
                sortBy={sortBy}
                descending={descending}
            />
            <UserTable
                users={users}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadUsers}
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
                loadData={loadUsers}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete User"
                message="Are you sure you want to delete this user?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadUsers}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete User Permanent"
                message="Are you sure you want to delete Permanent this user?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadUsers}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore User"
                message="Are you sure you want to restore this user?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadUsers}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default UserList;