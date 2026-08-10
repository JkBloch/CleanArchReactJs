import { Link } from "react-router-dom";
import { FaRegEye, FaMinusCircle, FaEdit, FaUndo, FaRegTimesCircle } from "react-icons/fa";

function UserTable({
    users,
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
                        onClick={() => onSortData("firstname")}
                    >
                        FirstName
                    </th>

                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("lastname")}
                    >
                        LastName
                    </th>
                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("username")}
                    >
                        UserName
                    </th>
                    <th
                        style={{ cursor: "pointer" }}
                        onClick={() => onSortData("email")}
                    >
                        email
                    </th>
                    <th >

                        Action

                    </th>

                </tr>

            </thead>

            <tbody>

                {
                    users.map(user => (

                        <tr key={user.id}>

                            <td>
                                {user.firstName}
                            </td>

                            <td>
                                {user.lastName}

                            </td>
                            <td>
                                {user.userName}

                            </td>
                            <td>
                                {user.email}

                            </td>

                            <td>
                                <div>
                                    <Link to={`/users/${user.id}`}
                                        className="icon-btn-info icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaRegEye></FaRegEye>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            View
                                        </span>
                                    </Link>

                                    <Link to={`/users/edit/${user.id}`}
                                        className="icon-btn-warning icon-btn no-underline">
                                        <span className="icon-section icon-section-sm">
                                            <FaEdit></FaEdit>
                                        </span>
                                        <span className="text-section text-section-sm">
                                            Edit
                                        </span>
                                    </Link>
                                    {

                                        user.isDeleted
                                            ?
                                            <button className="icon-btn-success icon-btn"
                                                onClick={() => onRestore(user.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaUndo ></FaUndo>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Restore
                                                </span>
                                            </button>
                                            :

                                            <button className="icon-btn-danger icon-btn"
                                                onClick={() => onDelete(user.id)}>
                                                <span className="icon-section icon-section-sm">
                                                    <FaMinusCircle ></FaMinusCircle>
                                                </span>
                                                <span className="text-section text-section-sm">
                                                    Delete
                                                </span>
                                            </button>

                                    }

                                    <button className="icon-btn-danger icon-btn"
                                        onClick={() => onDeletePermanent(user.id)}>
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

export default UserTable;