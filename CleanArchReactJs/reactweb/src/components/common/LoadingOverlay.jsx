import LoadingOverlayLib
    from "react-loading-overlay-ts";

import { useLoading }
    from "../../context/LoadingContext";

function LoadingOverlay({

    children

}) {

    const {

        loading

    } = useLoading();

    return (

        <LoadingOverlayLib

            active={loading}

            spinner

            text="Please wait..."

        >

            {children}

        </LoadingOverlayLib>

    );

}

export default LoadingOverlay;