import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import StateForm from "../state/StateForm";
import { getState, updateState } from "../.././../api/master/stateApi";
import { notify } from "../../../services/notificationService";

function StateEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [state, setState] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadState();

    }, []);

    async function loadState() {

        const response = await getState(id);

        setState(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateState(id, data);
            notify.success(

                "State updated successfully."

            );


            navigate("/states");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit State</h2>

            <StateForm
                initialValues={state}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default StateEdit;