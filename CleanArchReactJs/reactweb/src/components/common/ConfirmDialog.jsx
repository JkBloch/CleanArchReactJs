function ConfirmDialog({

    show,
    title,
    message,
    confirmText = "Yes",
    cancelText = "Cancel",
    confirmVariant,
    onConfirm,
    loadData,
    pageNumber,
    onCancel,
    loading = false

}) {
    const buttonStyle = `btn btn-${confirmVariant ? confirmVariant : 'danger'}`;
    if (!show)
        return null;
    function onConfirmation()
    {
        onConfirm();
        loadData(pageNumber)
    }
    return (

        <>
            <div
                className="modal fade show d-block"
                tabIndex="-1"
                style={{ backgroundColor: "rgba(0,0,0,.5)" }}
            >

                <div className="modal-dialog">

                    <div className="modal-content">

                        <div className="modal-header">

                            <h5 className="modal-title">

                                {title}

                            </h5>

                            <button
                                className="btn-close"
                                onClick={onCancel}
                            />

                        </div>

                        <div className="modal-body">

                            <p>{message}</p>

                        </div>

                        <div className="modal-footer">

                            <button
                                className="btn btn-secondary"
                                onClick={onCancel}
                                disabled={loading}
                            >
                                {cancelText}
                            </button>

                            <button
                                className={buttonStyle}
                                onClick={onConfirmation}
                                disabled={loading}
                            >
                                {loading
                                    ? "Processing..."
                                    : confirmText}
                            </button>

                        </div>

                    </div>

                </div>

            </div>
        </>

    );

}

export default ConfirmDialog;