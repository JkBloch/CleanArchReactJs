import { useEffect, useState } from "react";
import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";
import { getStates } from "../../../api/master/stateApi";
import { EMPTY_GUID } from "../../../constants/common";
function CitySearch({ handleChange, filters, loadCities,
    loading,
    setLoading,
    keyword,
    searchStateId,
    searchcode,
    searchname,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {
    const [states, setStates] = useState([]); 
    const loadState = async () => {
        try {
            var stateResponse = await getStates();
            setStates(stateResponse.data.data);
        }
        catch (error) {
            //notify.error("Failed to load States");
        }
    };
    
    useEffect(() => {
        loadState(); 
    }, []);

    async function handleSearch(e) {
        setLoading(true);
        await loadCities(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
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
        await downloadExcel(revfilters, "CityReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            stateId: searchStateId,
            code: searchcode,
            name: searchname,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadPdf(revfilters, "CityReport");
        setLoading(false);
    }
    if (loading)
        return <Loader />;
    return (

        <div className="card mb-2">

            <div className="card-body">

                <div className="row">

                    <div className="col-sm-2">

                        <input
                            className="form-control"
                            name="keyword"
                            placeholder="Search city..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>
                    <div className="col-sm-2">
                        <select
                            className="form-select"
                            name="stateId"
                            value={filters.stateId}
                            onChange={handleChange}
                        >
                            <option value={EMPTY_GUID} >-- Select State --</option>

                            {states.map(state => (
                                <option key={state.id} value={state.id}>
                                    {state.name}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className="col-sm-2">
                        <input
                            className="form-control"
                            name="code"
                            placeholder="Code"
                            value={filters.code}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-sm-2">
                        <input
                            className="form-control"
                            name="name"
                            placeholder="Name"
                            value={filters.name}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-sm-4">
                        <div className="row">
                            <div className="col-sm-4 pe-0 ps-0 me-2">
                                <button className="icon-btn icon-btn-info"
                                    onClick={handleSearch}>
                                    <span className="icon-section">
                                        <FaSearch> </FaSearch>
                                    </span>
                                    <span className="text-section">
                                        Search
                                    </span>
                                </button>
                            </div>
                            <div className="col-sm-4  pe-0 ps-0">
                                <button className="icon-btn icon-btn-success"
                                    onClick={handleExcelExport}>
                                    <span className="icon-section">
                                        <FaFileExcel> </FaFileExcel>
                                    </span>
                                    <span className="text-section">
                                        Excel
                                    </span>
                                </button>
                            </div>
                            <div className="col-sm-3  pe-0 ps-0">
                                <button className="icon-btn icon-btn-danger"
                                    onClick={handlePdfExport}>
                                    <span className="icon-section">
                                        <FaFilePdf> </FaFilePdf>
                                    </span>
                                    <span className="text-section">
                                        Pdf
                                    </span>
                                </button>
                            </div>
                        </div>
                    </div>
                </div>

            </div>

        </div>

    );
}

export default CitySearch;