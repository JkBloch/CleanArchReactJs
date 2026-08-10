import { useEffect, useState } from "react";
import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";
import { getRoles } from "../../../api/admin/roleApi";
import { notify } from "../../../services/notificationService";
import { getUsers } from "../../../api/admin/userApi";
import { EMPTY_GUID } from "../../../constants/common";
function UserRoleSearch({ handleChange, filters, loadUserRoles,
    loading,
    setLoading,
    keyword,
    searchRoleId,
    searchUserId,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {

    const [roles, setRoles] = useState([]);
    const [users, setUsers] = useState([]);
    const loadRole = async () => {
        try {
            var roleResponse = await getRoles();
            setRoles(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role data");
        }
    };
    const loadUser = async () => {
        try {
            var roleResponse = await getUsers();
            setUsers(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role Users");
        }
    };
    useEffect(() => {
        loadRole();
        loadUser();
    }, []);
    async function handleSearch(e) {
        setLoading(true);
        await loadUserRoles(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            roleId: searchRoleId,
            userId: searchUserId,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadExcel(revfilters, "UserRoleReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            roleId: searchRoleId,
            userId: searchUserId,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadPdf(revfilters, "UserRoleReport");
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
                            placeholder="Search userRole..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>
                    <div className="col-md-2">
                        <select
                            className="form-select"
                            name="userId"
                            value={filters.userId}
                            onChange={handleChange}
                        >
                            <option value={EMPTY_GUID} >-- Select User --</option>

                            {users.map(user => (
                                <option key={user.id} value={user.id}>
                                    {user.userName}
                                </option>
                            ))}
                        </select>


                    </div>
                    <div className="col-md-2">
                        <select
                            className="form-select"
                            name="roleId"
                            value={filters.roleId}
                            onChange={handleChange}
                        >
                            <option value={EMPTY_GUID} >-- Select Role --</option>

                            {roles.map(role => (
                                <option key={role.id} value={role.id}>
                                    {role.name}
                                </option>
                            ))}
                        </select>
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

export default UserRoleSearch;