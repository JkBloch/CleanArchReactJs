import { useEffect, useState } from "react";
import { NavLink } from "react-router-dom";
import { deleteCity, restoreCity, searchCities, deletePermanentCities } from "../../../api/master/cityApi";
import Loader from "../../../components/common/Loader";
import CityTable from "../../../components/master/city/CityTable";
import { FaPlus } from "react-icons/fa";
import CitySearch from "../city/CitySearch";
import Pagination from "../../../components/common/Pagination";
import ConfirmDialog from "../../../components/common/ConfirmDialog";
import { notify } from "../../../services/notificationService";
import { useLoading } from "../../../context/LoadingContext";
import { EMPTY_GUID } from "../../../constants/common";
//import { getErrorMessage } from "../../../utils/errorHandling";
function CityList() {
    const [selectedId, setSelectedId] = useState(null);
    const [showConfirmDelete, setShowConfirmDelete] = useState(false);
    const [showConfirmDeletePermanent, setShowConfirmDeletePermanent] = useState(false);
    const [showConfirmRestore, setShowConfirmRestore] = useState(false);
    const [cities, setCities] = useState([]);
    const [totalPages, setTotalPages] = useState(0);


    const [keyword, setKeyword] = useState("");
    const [searchStateId, setSearchStateId] = useState(EMPTY_GUID);
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
            case "stateId":
                setSearchStateId(value);
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
    async function loadCities(selectedPageNumber = 1, sortBy = "code", descending = false) {

        try {
            showLoading();
            const revfilters = {
                keyword: keyword,
                stateId:searchStateId,
                code: searchcode,
                name: searchname,
                pageNumber: selectedPageNumber,
                pageSize: pageSize,
                sortBy: sortBy,
                descending: descending
            }
            const response = await searchCities(revfilters);

            const result = response.data.data;

            setCities(result.items);

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

        loadCities();

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
        await deleteCity(selectedId);
        notify.success("City deleted successfully.");
        setShowConfirmDelete(false);
    };
    const handleConfirmDeletePermanent = async () => {
        try {
            await deletePermanentCities(selectedId);
            notify.success("City Permanent deleted successfully.");
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
        await restoreCity(selectedId);
        notify.success("City resore successfully.");
        setShowConfirmRestore(false);
    };

    if (loading)
        return <Loader />;

    return (

        <div className="container-fluid">

            <div className="d-flex justify-content-between mb-3">

                <h2>
                    Cities
                </h2>
                <div>
                    <NavLink to="/cities/create"
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
            <CitySearch
                handleChange={handleChange}
                filters={filters}
                loadCities={loadCities}
                loading={loading}
                setLoading={setLoading}
                keyword={keyword}
                searchStateId={searchStateId}
                searchcode={searchcode}
                searchname={searchname}
                selectedPageNumber={pageNumber}
                pageSize={pageSize}
                sortBy={sortBy}
                descending={descending}
            />
            <CityTable
                cities={cities}
                onDelete={handleDelete}
                onDeletePermanent={handleDeletePermanent}
                onRestore={handleRestore}
                loadData={loadCities}
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
                loadData={loadCities}
            />
            <ConfirmDialog
                show={showConfirmDelete}
                title="Delete City"
                message="Are you sure you want to delete this city?"
                confirmText="Delete"
                confirmVariant="danger"
                onConfirm={handleConfirmDelete}
                loadData={loadCities}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDelete(false)}
            />
            <ConfirmDialog
                show={showConfirmDeletePermanent}
                title="Delete City Permanent"
                message="Are you sure you want to delete Permanent this city?"
                confirmText="Delete Permanent"
                confirmVariant="danger"
                onConfirm={handleConfirmDeletePermanent}
                loadData={loadCities}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmDeletePermanent(false)}
            />
            <ConfirmDialog
                show={showConfirmRestore}
                title="Restore City"
                message="Are you sure you want to restore this city?"
                confirmText="Resote"
                confirmVariant="success"
                onConfirm={handleConfirmRestore}
                loadData={loadCities}
                pageNumber={pageNumber}
                onCancel={() => setShowConfirmRestore(false)}
            />
        </div>

    );
}

export default CityList;