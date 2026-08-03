import ConfirmDialog from "../../components/common/ConfirmDialog";

function EmployeeDeleteModal({

    show,
    employee,
    onDelete,
    onCancel,
    loading

}) {

    return (

        <ConfirmDialog

            show={show}

            title="Delete Employee"

            message={`Are you sure you want to delete ${employee?.firstName} ${employee?.lastName}?`}

            confirmText="Delete"

            onConfirm={onDelete}

            onCancel={onCancel}

            loading={loading}

        />

    );

}

export default EmployeeDeleteModal;