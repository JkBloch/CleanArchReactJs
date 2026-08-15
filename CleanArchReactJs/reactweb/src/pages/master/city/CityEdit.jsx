import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import CityForm from "../city/CityForm";
import { getCity, updateCity } from "../.././../api/master/cityApi";
import { notify } from "../../../services/notificationService";

function CityEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [city, setCity] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadCity();

    }, []);

    async function loadCity() {

        const response = await getCity(id);

        setCity(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateCity(id, data);
            notify.success(

                "City updated successfully."

            );


            navigate("/cities");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit City</h2>

            <CityForm
                initialValues={city}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default CityEdit;