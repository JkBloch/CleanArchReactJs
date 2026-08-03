import { useEffect, useState } from "react";
import { FaSearch } from "react-icons/fa";

function PermissionSearch({ filters, onSearch, handleChange }) {
    useEffect(() => {
        const timer = setTimeout(() => {
            onSearch(filters);
        }, 500);

        return () => clearTimeout(timer);
    }, []);

    function handleSearch(e) {     
        onSearch();

    }
    return (

        <div className="card mb-3">

            <div className="card-body">

                <div className="row">

                    <div className="col-md-3">

                        <input
                            className="form-control"
                            name="keyword"
                            placeholder="Search permission..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-3">
                        <input
                            className="form-control"
                            name="code"
                            placeholder="Code"
                            value={filters.code}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-3">
                        <input
                            className="form-control"
                            name="name"
                            placeholder="Name"
                            value={filters.name}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="col-md-3">
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
                </div>

            </div>

        </div>

    );
}

export default PermissionSearch;