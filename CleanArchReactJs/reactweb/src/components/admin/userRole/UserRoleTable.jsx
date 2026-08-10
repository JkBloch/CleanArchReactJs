import { Link } from "react-router-dom";
import { FaRegEye, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";
function UserRoleTable({
    userRoles,
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
                        onClick={() => onSortData("user")}
                    >
                        User
                    </th>
                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("role")}
                    >
                        Role
                    </th>

                    <th >

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    userRoles.map(userRole => (

                        <tr key={userRole.id}>

                            <td>
                                {userRole.userName}

                            </td>
                            <td>
                                {userRole.roleName}
                            </td>
                            <td>
                                <div>
                                    <Link to={`/userRoles/${userRole.id}`}
                                        className="icon-btn-info icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaRegEye></FaRegEye>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            View
                                        </span>
                                    </Link>

                                    <Link to={`/userRoles/edit/${userRole.id}`}
                                        className="icon-btn-warning icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaEdit></FaEdit>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Edit
                                        </span>
                                    </Link>
                                    {

                                        userRole.isDeleted
                                            ?
                                            <button className="icon-btn-success icon-btn"
                                                onClick={() => onRestore(userRole.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaUndo ></FaUndo>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Restore
                                                </span>
                                            </button>
                                            :

                                            <button className="icon-btn-danger icon-btn"
                                                onClick={() => onDelete(userRole.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaMinusCircle ></FaMinusCircle>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Delete
                                                </span>
                                            </button>

                                    }

                                    <button className="icon-btn-danger icon-btn"
                                        onClick={() => onDeletePermanent(userRole.id)}>
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

export default UserRoleTable;