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
    async function loadPermissions(selectedPageNumber = 1, sortBy = "code", descending=false) {

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

    const handleDelete=(id)=> {
        setSelectedId(id);
        setShowConfirmDelete(true);
    };
    const handleDeletePermanent = (id) =>{
        setSelectedId(id);
        setShowConfirmDeletePermanent(true);
    };
    const handleRestore=(id)=> {
        setSelectedId(id);
        setShowConfirmRestore(true);
    };

    const handleConfirmDelete = async () => {
        await deletePermission(selectedId);
        notify.success("Permission deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        await deletePermanentPermissions(selectedId);
        notify.success("Permission Permanent deleted successfully.");
        setShowConfirmDeletePermanent(false);
    };

    const handleConfirmRestore = async () => {
        await restorePermission(selectedId);
        notify.success("Permission resore successfully.");
        setShowConfirmRestore(false);
    };    

    function sort(column) {
        if (column === sortBy) {
            setDescending(!descending);
        }
        setSortBy(column);

        //setDescending(descending)
        //const [pageSize, setPageSize] = useState(10);
        //const [sortBy, setSortBy] = useState("code");
        //const [descending, setDescending] = useState(false);
        //se
        //setFilters(prev => ({

        //    ...prev,

        //    sortBy: column,

        //    descending:
        //        prev.sortBy === column
        //            ? !prev.descending
        //            : false
        //}));
       // loadPermissions();
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
                loadPermissions={loadPermissions}
            />
            <PermissionTable
                permissions={permissions}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadPermissions}
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
                loadData={loadPermissions}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete Permission"
                message="Are you sure you want to delete this permission?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadPermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete Permission Permanent"
                message="Are you sure you want to delete Permanent this permission?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadPermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore Permission"
                message="Are you sure you want to restore this permission?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadPermissions}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default PermissionList;