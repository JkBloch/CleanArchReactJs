import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteRole, restoreRole, searchRoles, deletePermanentRoles } from "../../../api/admin/roleApi";
import Loader from "../../../components/common/Loader";
import RoleTable from "../../../components/admin/role/RoleTable";
import { FaPlus } from "react-icons/fa";
import RoleSearch from "../role/RoleSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";

function RoleList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [roles, setRoles] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchcode, setSearchcode] = useState("");
    const [searchname, setSearchname] = useState("");
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("code");
    const [descending, setDescending] = useState(false);

    const [filters, setFilters] = useState({

        keyword: "",
        code: "",
        name: "",
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
            case "code":
                setSearchcode(value);
                break;
            case "name":
                setSearchname(value);
                break;
            default:
        }
        setFilters(prev => ({
            ...prev,
            [name]: value,
            pageNumber: 1
        }));

    }
    async function loadRoles(selectedPageNumber = 1, sortBy = "code", descending = false) {

        try {
           
            const revfilters = {
                keyword: keyword,
                code: searchcode,
                name: searchname,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchRoles(revfilters);

            const result = response.data.data;

            setRoles(result.items);

            setTotalPages(result.totalPages);

        } finally {

            setLoading(false);

        }
    }

    useEffect(() => {

        loadRoles();

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
        await deleteRole(selectedId);
        notify.success("Role deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentRoles(selectedId);
            notify.success("Role Permanent deleted successfully.");
            setShowConfirmDeletePermanent(false);
        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setShowConfirmDeletePermanent(false);
        }
        finally {
            setShowConfirmDeletePermanent(false);
        }
      
    };

    const handleConfirmRestore = async () => {
        await restoreRole(selectedId);
        notify.success("Role resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    Roles
                </h2>
                <div>
                    <NavLink to="/roles/create"
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
            <RoleSearch
                handleChange={handleChange}
                filters={filters}
                loadRoles={loadRoles}
                loading={loading}
                setLoading={setLoading}
                keyword={keyword}
                searchcode={searchcode}
                searchname={searchname}
                selectedPageNumber={pageNumber}
                pageSize={pageSize}
                sortBy={sortBy}
                descending={descending}
            />
            <RoleTable
                roles={roles}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadRoles}
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
                loadData={loadRoles}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete Role"
                message="Are you sure you want to delete this role?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete Role Permanent"
                message="Are you sure you want to delete Permanent this role?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore Role"
                message="Are you sure you want to restore this role?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadRoles}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default RoleList;