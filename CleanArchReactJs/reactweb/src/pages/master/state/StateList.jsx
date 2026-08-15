import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteState, restoreState, searchStates, deletePermanentStates } from "../../../api/master/stateApi";
import Loader from "../../../components/common/Loader";
import StateTable from "../../../components/master/state/StateTable";
import { FaPlus } from "react-icons/fa";
import StateSearch from "../state/StateSearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import { useLoading } from "../../../context/LoadingContext";
//import { getErrorMessage } from "../../../utils/errorHandling";
function StateList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [states, setStates] = useState([]);
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
    async function loadStates(selectedPageNumber = 1, sortBy = "code", descending = false) {

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
            const response = await searchStates(revfilters);

            const result = response.data.data;

            setStates(result.items);

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

        loadStates();

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
        await deleteState(selectedId);
        notify.success("State deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentStates(selectedId);
            notify.success("State Permanent deleted successfully.");
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
        await restoreState(selectedId);
        notify.success("State resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    States
                </h2>
                <div>
                    <NavLink to="/states/create"
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
            <StateSearch
                handleChange={handleChange}
                filters={filters}
                loadStates={loadStates}
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
            <StateTable
                states={states}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadStates}
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
                loadData={loadStates}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete State"
                message="Are you sure you want to delete this state?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadStates}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete State Permanent"
                message="Are you sure you want to delete Permanent this state?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadStates}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore State"
                message="Are you sure you want to restore this state?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadStates}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default StateList;