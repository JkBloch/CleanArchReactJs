import { Link } from "react-router-dom";
FaRegEye
import {
    FaRegEye,
    FaMinusCircle,
    FaEdit,
    FaUndo,
    FaRegTimesCircle
} from "react-icons/fa";
function PermissionTable({
    permissions,
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
                        onClick={() => onSortData("code")}
                    >
                        Code
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("name")}
                    >
                        Name
                    </th>
                    <th >

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    permissions.map(permission => (

                        <tr key={permission.id}>

                            <td>
                                {permission.code}
                            </td>

                            <td>
                                {permission.name} 

                            </td>                         

                            <td>
                            <div>
                                <Link to={`/permissions/${permission.id}`}
                                        className="icon-btn-info icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaRegEye></FaRegEye>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            View
                                        </span>
                                    </Link>

                                <Link to={`/permissions/edit/${permission.id}`}
                                        className="icon-btn-warning icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaEdit></FaEdit>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Edit
                                        </span>
                                    </Link>
                                    {

                                        permission.isDeleted
                                            ?
                                               <button className="icon-btn-success icon-btn"
                                                onClick={() => onRestore(permission.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaUndo ></FaUndo>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Restore
                                                </span>
                                            </button>    
                                            :

                                            <button className="icon-btn-danger icon-btn"
                                                onClick={() => onDelete(permission.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaMinusCircle ></FaMinusCircle>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Delete
                                                </span>
                                            </button>

                                    }

                                    <button className="icon-btn-danger icon-btn"
                                        onClick={() => onDeletePermanent(permission.id)}>
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

export default PermissionTable;