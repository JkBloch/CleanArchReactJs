import ConfirmDialog from "../../../components/common/ConfirmDialog";

function PermissionDeleteModal({

    show,
    permission,
    onDelete,
    onCancel,
    loading

}) {

    return (

        <ConfirmDialog

            show={show}

            title="Delete Permission"

            message={`Are you sure you want to delete ${permission?.name}  ?`}

            confirmText="Delete"

            onConfirm={onDelete}

            onCancel={onCancel}

            loading={loading}

        />

    );

}

export default PermissionDeleteModal;