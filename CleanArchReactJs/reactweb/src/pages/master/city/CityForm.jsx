import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa";
import { getStates } from "../../../api/master/stateApi";
function CityForm({
    initialValues,
    onSubmit,
    loading
}) {
    const [states, setStates ] = useState([]);
    const [city, setCity] = useState(initialValues);
    useEffect(() => {
        loadState();
        setCity(initialValues);
    }, [initialValues]);

    const loadState = async () => {
        try {
            var stateResponse = await getStates();
            setStates(stateResponse.data.data);
        }
        catch (error) {
            notify.error("Failed to load States");
        }
    };
    function handleChange(e) {

        const { name, value } = e.target;

        setCity(prev => ({
            ...prev,
            [name]: value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(city);
        setCity(city);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">
                <div className="col-md-6 mb-3">

                    <label>State</label>
                    <select
                        className="form-select"
                        name="stateId"
                        value={city.stateId}
                        onChange={handleChange}
                    >
                        <option value="">-- Select State --</option>

                        {states.map(state => (
                            <option key={state.id} value={state.id}>
                                {state.name}
                            </option>
                        ))}
                    </select>

                </div>
           
                <div className="col-md-6 mb-3">

                    <label>Code</label>

                    <input
                        className="form-control"
                        name="code"
                        value={city.code}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Name</label>

                    <input
                        className="form-control"
                        name="name"
                        value={city.name}
                        onChange={handleChange}
                        required
                    />

                </div>

            </div>
            <div>
                <button
                    className="icon-btn-success icon-btn"
                    disabled={loading}
                >
                    <span className="icon-section">
                        <FaRegSave></FaRegSave>
                    </span>
                    <span className="text-section">
                        Save
                    </span>
                </button>
                <Link to="/cities"
                    className="icon-btn-info icon-btn no-underline" >
                    <span className="icon-section">
                        <FaArrowLeft></FaArrowLeft>
                    </span>
                    <span className="text-section">
                        Go Back
                    </span>
                </Link>
            </div>
        </form>

    );
}

export default CityForm;