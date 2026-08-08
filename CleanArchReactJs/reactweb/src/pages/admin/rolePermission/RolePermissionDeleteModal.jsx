import ConfirmDialog from "../../../components/common/ConfirmDialog";

function RolePermissionDeleteModal({

    show,
    rolePermission,
    onDelete,
    onCancel,
    loading

}) {

    return (

        <ConfirmDialog

            show={show}

            title="Delete RolePermission"

            message={`Are you sure you want to delete ${rolePermission?.name}  ?`}

            confirmText="Delete"

            onConfirm={onDelete}

            onCancel={onCancel}

            loading={loading}

        />

    );

}

export default RolePermissionDeleteModal;