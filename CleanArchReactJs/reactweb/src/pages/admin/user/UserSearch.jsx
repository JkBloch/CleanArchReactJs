import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";

function UserSearch({ handleChange, filters, loadUsers,
    loading,
    setLoading,
    keyword,
    searchFirstName,
    searchLastName,
    searchUserName,
    searchEmail,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {
    async function handleSearch(e) {
        setLoading(true);
        await loadUsers(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            firstName : searchFirstName,
            lastName:searchLastName,
            userName:searchUserName,
            email:searchEmail,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }      
        await downloadExcel(revfilters, "UserReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
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
        await downloadPdf(revfilters, "UserReport");
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
                            placeholder="Search user..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="firstName"
                            placeholder="FirstName"
                            value={filters.firstName}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="lastName"
                            placeholder="LastName"
                            value={filters.lastName}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="userName"
                            placeholder="UserName"
                            value={filters.userName}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="email"
                            placeholder="Email"
                            value={filters.email}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-5 mt-3">
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

export default UserSearch;