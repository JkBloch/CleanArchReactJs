import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createState } from "../.././../api/master/stateApi";
import StateForm from "../state/StateForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function StateCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: ""
    });

    async function save(state) {

        setLoading(true);

        try {

            await createState(state);
            notify.success(
                "State created successfully."
            );

         //   navigate("/states");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setInitialValues(state);

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add State</h2>

            <StateForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default StateCreate;