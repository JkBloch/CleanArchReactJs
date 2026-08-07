import ConfirmDialog from "../../../components/common/ConfirmDialog";

function RoleDeleteModal({

    show,
    role,
    onDelete,
    onCancel,
    loading

}) {

    return (

        <ConfirmDialog

            show={show}

            title="Delete Role"

            message={`Are you sure you want to delete ${role?.name}  ?`}

            confirmText="Delete"

            onConfirm={onDelete}

            onCancel={onCancel}

            loading={loading}

        />

    );

}

export default RoleDeleteModal;