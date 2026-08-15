import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteDepartment, restoreDepartment, searchDepartments, deletePermanentDepartments } from "../../../api/master/departmentApi";
import Loader from "../../../components/common/Loader";
import DepartmentTable from "../../../components/master/department/DepartmentTable";
import { FaPlus } from "react-icons/fa";
import DepartmentSearch from "../department/DepartmentSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import { useLoading } from "../../../context/LoadingContext";
//import { getErrorMessage } from "../../../utils/errorHandling";
function DepartmentList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [departments, setDepartments] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchcode, setSearchcode] = useState("");
    const [searchname, setSearchname] = useState("");
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize, setPageSize] = useState(10);
    const [sortBy, setSortBy] = useState("code");
    const [descending, setDescending] = useState(false);
    const { showLoading, hideLoading } = useLoading();
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
    async function loadDepartments(selectedPageNumber = 1, sortBy = "code", descending = false) {

        try {
            showLoading();
            const revfilters = {
                keyword: keyword,
                code: searchcode,
                name: searchname,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchDepartments(revfilters);

            const result = response.data.data;

            setDepartments(result.items);

            setTotalPages(result.totalPages);

        } catch (error) {
            hideLoading();
            setLoading(false);

        }

        finally {
            hideLoading();
            setLoading(false);

        }
    }

    useEffect(() => {

        loadDepartments();

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
        await deleteDepartment(selectedId);
        notify.success("Department deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentDepartments(selectedId);
            notify.success("Department Permanent deleted successfully.");
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
        await restoreDepartment(selectedId);
        notify.success("Department resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    Departments
                </h2>
                <div>
                    <NavLink to="/departments/create"
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
            <DepartmentSearch
                handleChange={handleChange}
                filters={filters}
                loadDepartments={loadDepartments}
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
            <DepartmentTable
                departments={departments}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadDepartments}
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
                loadData={loadDepartments}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete Department"
                message="Are you sure you want to delete this department?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadDepartments}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete Department Permanent"
                message="Are you sure you want to delete Permanent this department?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadDepartments}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore Department"
                message="Are you sure you want to restore this department?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadDepartments}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default DepartmentList;