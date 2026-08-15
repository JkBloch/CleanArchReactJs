import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createCity } from "../.././../api/master/cityApi";
import CityForm from "../city/CityForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function CityCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: ""
    });

    async function save(city) {

        setLoading(true);

        try {

            await createCity(city);
            notify.success(
                "City created successfully."
            );

            //   navigate("/cities");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setInitialValues(city);

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add City</h2>

            <CityForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default CityCreate;