import { useEffect, useState } from "react";
import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";

function RoleSearch({ handleChange, filters, loadRoles,
    loading,
    setLoading,
    keyword,
    searchcode,
    searchname,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {
    async function handleSearch(e) {
        setLoading(true);
        await loadRoles(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            code: searchcode,
            name: searchname,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        //var roles = loadRoles(pageNumber, sortBy, descending);
        await downloadExcel(revfilters, "RoleReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            code: searchcode,
            name: searchname,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadPdf(revfilters, "RoleReport");
        setLoading(false);
    }
    if (loading)
        return <Loader />;
    return (

        <div className="card mb-3">

            <div className="card-body">

                <div className="row">

                    <div className="col-md-3">

                        <input
                            className="form-control"
                            name="keyword"
                            placeholder="Search role..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="code"
                            placeholder="Code"
                            value={filters.code}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="name"
                            placeholder="Name"
                            value={filters.name}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-5">
                        <div className="row">
                            <div className="col">
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
                            <div className="col">
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
                            <div className="col">
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

export default RoleSearch;