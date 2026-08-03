import { useEffect, useState } from "react";
import { Link, NavLink } from "react-router-dom";
import { getPermissions, deletePermission, restorePermission, searchPermissions, deletePermanentPermissions } from "../../../api/permissionApi";
import Loader from "../../../components/common/Loader";
import PermissionTable from "../../../components/admin/permission/PermissionTable";
import { FaPlus } from "react-icons/fa";
import PermissionSearch from "../permission/PermissionSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import IconButton from "../../../components/common/IconButton";
function PermissionList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [permissions, setPermissions] = useState([]);
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
        sortBy: "code",
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
    async function loadPermissions(selectedPageNumber=1) {

        try {

            //const response = await getPermissions();
            //setPermissions(response.data.data ?? []);
            const revfilters = { 
                    keyword: keyword,
                    code: searchcode,
                    name: searchname,
                pageNumber: selectedPageNumber,
                    pageSize: pageSize,
                    sortBy: sortBy,
                    descending: descending 
            } 
            const response = await searchPermissions(revfilters);

            const result = response.data.data;

            setPermissions(result.items);

            setTotalPages(result.totalPages);

        } finally {

            setLoading(false);

        }
    }

    useEffect(() => {

        loadPermissions();

    }, []);

    async function handleDelete(id) {
        setSelectedId(id);
        setShowConfirmDelete(true); 
    }
    async function handleDeletePermanent(id) {
        setSelectedId(id);
        setShowConfirmDeletePermanent(true);
    }
    const handleConfirmDelete = async () => {

        await deletePermission(selectedId);

        notify.success("Permission deleted successfully.");

        setShowConfirmDelete(false);

        loadPermissions(pageNumber);
    };
    const handleConfirmDeletePermanent = async () => {

        await deletePermanentPermissions(selectedId);

        notify.success("Permission deleted successfully.");

        setShowConfirmDeletePermanent(false);

        loadPermissions(pageNumber);
    };
    async function handleRestore(id) {
        setSelectedId(id);
        setShowConfirmRestore(true);         
    }
    const handleConfirmRestore = async () => {
        await restorePermission(selectedId);

        notify.success("Permission resore successfully.");

        setShowConfirmRestore(false);

        loadPermissions(pageNumber);
    };   
   
    function handleSearch() {
        setPageNumber(1);
        loadPermissions(1);
    }
    function changePage(page) {
        setPageNumber(page);     
        loadPermissions(page);
    }

    function sort(column) {

        setFilters(prev => ({

            ...prev,

            sortBy: column,

            descending:
                prev.sortBy === column
                    ? !prev.descending
                    : false
        }));
    }

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    Permissions                    
                </h2>
                <div>
                    <NavLink to="/permissions/create"
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
            <PermissionSearch
                handleChange={handleChange }
                filters={filters}
                onSearch={handleSearch}
            />
            <PermissionTable
                permissions={permissions}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onSort={sort}
                onRestore={handleRestore }
            />
            <Pagination
                pageNumber={pageNumber}
                totalPages={totalPages}
                onPageChange={changePage}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete Permission"
                message="Are you sure you want to delete this permission?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete Permission Permanent"
                message="Are you sure you want to delete Permanent this permission?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore Permission"
                message="Are you sure you want to restore this permission?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default PermissionList;