import { Link } from "react-router-dom";
FaRegEye
import {
    FaRegEye,
    FaMinusCircle,
    FaEdit,
    FaUndo,
    FaRegTimesCircle
} from "react-icons/fa";
function RolePermissionTable({
    rolePermissions,
    onDelete,
    onDeletePermanent,
    onRestore,
    loadData,
    pageNumber,
    sortBy,
    setSortBy,
    descending,
    setDescending

}) {
    const onSortData = (column) => {
        var des = descending;
        if (column === sortBy) {
            setDescending(!descending);
            des = !descending;
        }
        setSortBy(column);
        loadData(pageNumber, column, des)
    }
    return (

        <table className="table table-striped table-hover">

            <thead className="table-dark">

                <tr>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("role")}
                    >
                        Role
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("permission")}
                    >
                        Permission
                    </th>
                    <th >

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    rolePermissions.map(rolePermission => (

                        <tr key={rolePermission.id}>

                            <td>
                                {rolePermission.roleName}
                            </td>

                            <td>
                                {rolePermission.permissionName}

                            </td>

                            <td>
                                <div>
                                    <Link to={`/rolePermissions/${rolePermission.id}`}
                                        className="icon-btn-info icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaRegEye></FaRegEye>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            View
                                        </span>
                                    </Link>

                                    <Link to={`/rolePermissions/edit/${rolePermission.id}`}
                                        className="icon-btn-warning icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaEdit></FaEdit>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Edit
                                        </span>
                                    </Link>
                                    {

                                        rolePermission.isDeleted
                                            ?
                                            <button className="icon-btn-success icon-btn"
                                                onClick={() => onRestore(rolePermission.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaUndo ></FaUndo>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Restore
                                                </span>
                                            </button>
                                            :

                                            <button className="icon-btn-danger icon-btn"
                                                onClick={() => onDelete(rolePermission.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaMinusCircle ></FaMinusCircle>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Delete
                                                </span>
                                            </button>

                                    }

                                    <button className="icon-btn-danger icon-btn"
                                        onClick={() => onDeletePermanent(rolePermission.id)}>
                                        <span className="icon-section icon-section-sm">
                                            <FaRegTimesCircle ></FaRegTimesCircle>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Delete Permanent
                                        </span>
                                    </button>

                                </div>
                            </td>

                        </tr>

                    ))
                }

            </tbody>

        </table>

    );
}

export default RolePermissionTable;