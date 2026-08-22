import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { FaRegSave, FaArrowLeft } from "react-icons/fa";
import { getDepartments } from "../../../api/master/departmentApi"
import { getStates } from "../../../api/master/stateApi"
import { getCities, searchCities } from "../../../api/master/cityApi"
import { EMPTY_GUID } from "../../../constants/common"
import { notify } from "../../../services/notificationService";
import EmployeePhotoUpload from "../../../components/master/employee/EmployeePhotoUpload";

function EmployeeForm({
    initialValues,
    onSubmit,
    loading,
    loadEmployee 
}) {
    const [departments, setDepartments] = useState([]);
    const [states, setStates] = useState([]);
    const [cities, setCities] = useState([]);
    const [employee, setEmployee] = useState(initialValues);
    const [seletedStateId, setSeletedStateId] = useState([EMPTY_GUID]);
    const genderOptions = [
        { value: 1, label: "Male" },
        { value: 2, label: "Female" },
        { value: 3, label: "Other" }
    ];
    useEffect(() => {
        loadDepartment();
        loadState();
        loadCities(initialValues.stateId);
        setEmployee(initialValues);
    }, [initialValues]);
    const loadDepartment = async () => {
        try {
            var response = await getDepartments();
            setDepartments(response.data.data);
        }
        catch (error) {
            notify.error("Failed to load department data");
        }
    };
    const loadState = async () => {
        try {
            var response = await getStates();
            setStates(response.data.data);
        }
        catch (error) {
            notify.error("Failed to load state data");
        }
    };
    const loadCities = async (filterStateId ) => {
        try {
            if (filterStateId == '') {
                filterStateId = EMPTY_GUID;
            }
            const revfilters = {
                keyword: "",
                stateId: filterStateId,
                code: "",
                name: "",
                pageNumber: 1,
                pageSize: 100,
                sortBy: "name",
                descending: false
            }
            const response = await searchCities(revfilters);
            const result = response.data.data;
            setCities(result.items);
        }
        catch (error) {
            notify.error("Failed to load city data");
        }
    };
     
    function handleChange(e) {

        let { name, value } = e.target;

        if (name === "stateId") {
            loadCities(value);
        }
        if (name == "isActive") {
            value = e.target.checked;
        }
     
        setEmployee(prev => ({
            ...prev,
            [name]: (name == "gender") ? Number.parseInt(value, 10): value
        }));
    }

    function submit(e) {

        e.preventDefault();

        onSubmit(employee);
        setEmployee(employee);
    }

    return (

        <form onSubmit={submit}>

            <div className="row">
             
                <div className="col-md-6 mb-3">

                    <label>Code</label>

                    <input
                        className="form-control"
                        name="code"
                        value={employee.code}
                        onChange={handleChange}
                        required
                    />

                </div>

                <div className="col-md-6 mb-3">

                    <label>Name</label>

                    <input
                        className="form-control"
                        name="name"
                        value={employee.name}
                        onChange={handleChange}
                        required
                    />

                </div>

            
            <div className="col-md-6 mb-3">

                <label>Email</label>

                <input
                    className="form-control"
                    name="email"
                    value={employee.email}
                    onChange={handleChange}
                    required
                />

            </div>
            <div className="col-md-6 mb-3">

                <label>Phone Number</label>

                <input
                    className="form-control"
                        name="phoneNumber"
                    value={employee.phoneNumber}
                    onChange={handleChange}                    
                />

            </div>
            <div className="col-md-6 mb-3">
                <label>Department</label>
                <select
                    className="form-select"
                    name="departmentId"
                    value={employee.departmentId}
                    onChange={handleChange}
                >
                    <option value="">-- Select Department --</option>

                    {departments.map(department => (
                        <option key={department.id} value={department.id}>
                            {department.name}
                        </option>
                    ))}
                </select>
            </div>
            <div className="col-md-6 mb-3">
                <label>State</label>
                <select
                    className="form-select"
                    name="stateId"
                    value={employee.stateId}
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
                <label>City</label>
                <select
                    className="form-select"
                    name="cityId"
                    value={employee.cityId}
                    onChange={handleChange}
                >
                    <option value="">-- Select City --</option>

                    {cities.map(city => (
                        <option key={city.id} value={city.id}>
                            {city.name}
                        </option>
                    ))}
                </select>
            </div>
            <div className="col-md-6 mb-3">

                <label>Salary</label>

                <input
                    type="number"
                    className="form-control"
                    name="salary"
                    value={employee.salary}
                    onChange={handleChange}                    
                />

            </div>
            <div className="col-md-6 mb-3">

                <label>DateOfBirth</label>

                <input
                    type="date"
                    className="form-control"
                    name="dateOfBirth"
                        value={(employee.dateOfBirth !=null)?employee.dateOfBirth.substring(0, 10):""}
                    onChange={handleChange}
                />
            </div>
            <div className="col-md-6 mb-3">

                <label>JoiningDate</label>

                <input
                    type="date"
                    className="form-control"
                    name="joiningDate"
                        value={(employee.joiningDate != null)?employee.joiningDate.substring(0, 10):""}
                    onChange={handleChange}
                />
            </div>
            <div className="col-md-6 mb-3">

                <label>Gender</label>
                    <select name="gender" value={employee.gender}
                        className="form-select" onChange={handleChange} >
                        <option value={0} >Select Gender</option>
                        <option value={1} >Male</option>
                        <option value={2} >Female</option>
                        <option value={3} >Other</option>
                    </select>
                    <br></br>
                    <input
                        type="checkbox"
                        className="form-check-input"
                        name="isActive"
                        checked={employee.isActive}
                        value={employee.isActive}
                        onChange={handleChange} />
                    <label className="form-check-label" htmlFor="isActive" >
                        IsActive
                    </label>
                </div>
                {
                     (employee.id != null) && (                
                    <div className="col-md-4 m-2">

                        <EmployeePhotoUpload
                            employee={employee}
                            onPhotoChanged={
                                loadEmployee
                            }
                        />

                    </div> )
                }
            <div className="col-md-6 mb-3">
                <div className="row ms-2 mt-4">
                    <div className="col-md-3 form-check">
                        
                    </div>
                </div>
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
                <Link to="/employees"
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

export default EmployeeForm;