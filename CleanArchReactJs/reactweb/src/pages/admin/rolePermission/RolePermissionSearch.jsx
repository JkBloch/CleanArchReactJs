import { useEffect, useState } from "react";
import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";
import { getRoles } from "../../../api/admin/roleApi";
import { notify } from "../../../services/notificationService";
import { getPermissions } from "../../../api/admin/permissionApi";
import { EMPTY_GUID } from "../../../constants/common";
function RolePermissionSearch({ handleChange, filters, loadRolePermissions,
    loading,
    setLoading,
    keyword,
    searchRoleId,
    searchPermissionId,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {

    const [roles, setRoles] = useState([]);
    const [permissions, setPermissions] = useState([]);
    const loadRole = async () => {
        try {
            var roleResponse = await getRoles();
            setRoles(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role data");
        }
    };
    const loadPermission = async () => {
        try {
            var roleResponse = await getPermissions();
            setPermissions(roleResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load role Permissions");
        }
    };
    useEffect(() => {
        loadRole();
        loadPermission();
    }, []);
    async function handleSearch(e) {
        setLoading(true);
        await loadRolePermissions(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            roleId: searchRoleId,
            permissionId: searchPermissionId,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        //var rolePermissions = loadRolePermissions(pageNumber, sortBy, descending);
        await downloadExcel(revfilters, "RolePermissionReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            roleId: searchRoleId,
            permissionId: searchPermissionId,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadPdf(revfilters, "RolePermissionReport");
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
                            placeholder="Search rolePermission..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

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
                        {/*<input*/}
                        {/*    className="form-control"*/}
                        {/*    name="code"*/}
                        {/*    placeholder="Code"*/}
                        {/*    value={filters.code}*/}
                        {/*    onChange={handleChange}*/}
                        {/*/>*/}
                    </div>
                    <div className="col-md-2">
                        <select
                            className="form-select"
                            name="permissionId"
                            value={filters.permissionId}
                            onChange={handleChange}
                        >
                            <option value={EMPTY_GUID} >-- Select Permission --</option>

                            {permissions.map(permission => (
                                <option key={permission.id} value={permission.id}>
                                    {permission.name}
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

export default RolePermissionSearch;