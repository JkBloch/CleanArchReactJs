import { useState, useEffect } from "react";
import { FaSearch, FaFileExcel, FaFilePdf } from "react-icons/fa";
import { downloadExcel, downloadPdf } from "../../../api/common/exportApi";
import { getDepartments } from "../../../api/master/departmentApi";
import { getStates } from "../../../api/master/stateApi";
import { searchCities } from "../../../api/master/cityApi"
import { EMPTY_GUID } from "../../../constants/common";
function EmployeeSearch({ handleChange, filters, loadEmployees,
    loading,
    setLoading,
    keyword,
    searchcode,
    searchname,
    searchemail,
    searchDepartmentId,
    searchStateId,
    searchCityId,
    searchSalaryFrom,
    searchSalaryTo,
    searchDateOfBirthFrom,
    searchDateOfBirthTo,
    searchJoiningDateFrom,
    searchJoiningDateTo,
    selectedPageNumber,
    pageSize,
    sortBy,
    descending
}) {

    const [departments, setDepartments] = useState([]);
    const [states, setStates] = useState([]);
    const [cities, setCities] = useState([]);



    useEffect(() => {
        loadDepartment();
        loadState();
        loadCities(searchStateId); 
    }, []);

    async function handleStateChange(e) {
        let { name, value } = e.target;
        await loadCities(value);
        handleChange(e);
    }
    async function handleSearch(e) {
        setLoading(true);
        await loadEmployees(1, sortBy, descending);
        setLoading(false);
    }
    async function handleExcelExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            code: searchcode,
            name: searchname,
            email: searchemail,
            departmentId: searchDepartmentId,
            stateId: searchStateId,
            cityId: searchCityId,
            salaryFrom: searchSalaryFrom,
            salaryTo: searchSalaryTo,
            dateOfBirthFrom: searchDateOfBirthFrom,
            dateOfBirthTo: searchDateOfBirthTo,
            joiningDateFrom: searchJoiningDateFrom,
            joiningDateTo: searchJoiningDateTo,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadExcel(revfilters, "EmployeeReport");
        setLoading(false);
    }
    async function handlePdfExport(e) {
        setLoading(true);
        const revfilters = {
            keyword: keyword,
            code: searchcode,
            name: searchname,
            email: searchname,
            departmentId: searchDepartmentId,
            stateId: searchStateId,
            cityId: searchCityId,
            salaryFrom: searchSalaryFrom,
            salaryTo: searchSalaryTo,
            dateOfBirthFrom: searchDateOfBirthFrom,
            dateOfBirthTo: searchDateOfBirthTo,
            joiningDateFrom: searchJoiningDateFrom,
            joiningDateTo: searchJoiningDateTo,
            pageNumber: selectedPageNumber,
            pageSize: pageSize,
            sortBy: sortBy,
            descending: descending
        }
        await downloadPdf(revfilters, "EmployeeReport");
        setLoading(false);
    }
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
    const loadCities = async (filterStateId) => {
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
    if (loading)
        return <Loader />;
    return (

        <div className="card mb-3">

            <div className="card-body">

                <div className="row">

                    <div className="col-md-2">

                        <input
                            className="form-control"
                            name="keyword"
                            placeholder="Search employee..."
                            value={filters.keyword}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="code"
                            placeholder="Code"
                            value={filters.code}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="name"
                            placeholder="Name"
                            value={filters.name}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            className="form-control"
                            name="email"
                            placeholder="Email"
                            value={filters.email}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2"> 
                        <select
                            className="form-select"
                            name="departmentId"
                            value={filters.departmentId}
                            onChange={handleChange}
                        >
                            <option value={EMPTY_GUID} >-- Search Department --</option>

                            {departments.map(department => (
                                <option key={department.id} value={department.id}>
                                    {department.name}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className="col-md-2"> 
                        <button className="icon-btn icon-btn-success"
                            onClick={handleExcelExport}>
                            <span className="icon-section">
                                <FaFileExcel> </FaFileExcel>
                            </span>
                            <span className="text-section">
                                Excel
                            </span>
                        </button>
                    </div>
                   
                  
                </div>
                <div className="row  mt-2">
                    <div className="col-md-2">
                        <select
                            className="form-select"
                            name="stateId"
                            value={filters.stateId}
                            onChange={handleStateChange}
                        >
                            <option value={EMPTY_GUID} >-- Search State --</option>

                            {states.map(state => (
                                <option key={state.id} value={state.id}>
                                    {state.name}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className="col-md-2">                        
                        <select
                            className="form-select"
                            name="cityId"
                            value={filters.cityId}
                            onChange={handleChange} >
                            <option value="">-- Search City --</option>

                            {cities.map(city => (
                                <option key={city.id} value={city.id}>
                                    {city.name}
                                </option>
                            ))}
                        </select>
                    </div>
                    <div className="col-md-2">
                        <input
                            type="number"
                            className="form-control"
                            name="salaryFrom"
                            placeholder="Salary From"
                            value={filters.salaryFrom}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            type="number"
                            className="form-control"
                            name="salaryTo"
                            placeholder="Salary To"
                            value={filters.salaryTo}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">

                    </div>
                    <div className="col-md-2">
                        <button className="icon-btn icon-btn-danger"
                            onClick={handlePdfExport}>
                            <span className="icon-section">
                                <FaFilePdf> </FaFilePdf>
                            </span>
                            <span className="text-section">
                                Pdf
                            </span>
                        </button>
                    </div>

                </div>


                <div className="row mt-2">
                    <div className="col-md-2">
                        <input
                            type="date"
                            className="form-control"
                            name="dateOfBirthFrom"
                            placeholder="From DateOfBirth"
                            value={filters.dateOfBirthFrom}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            type="date"
                            className="form-control"
                            name="dateOfBirthTo"
                            placeholder="To DateOfBirth"
                            value={filters.dateOfBirthTo}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            type="date"
                            className="form-control"
                            name="joiningDateFrom"
                            placeholder="From JoiningDate"
                            value={filters.joiningDateFrom}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="col-md-2">
                        <input
                            type="date"
                            className="form-control"
                            name="joiningDateTo"
                            placeholder="To JoiningDate"
                            value={filters.joiningDateTo}
                            onChange={handleChange}
                        />
                    </div>

                    <div className="col-md-2">

                    </div>
                    <div className="col-md-2">
                        <button className="icon-btn icon-btn-info"
                            onClick={handleSearch}>
                            <span className="icon-section">
                                <FaSearch> </FaSearch>
                            </span>
                            <span className="text-section">
                                Search
                            </span>
                        </button>
                    </div>

                    
                </div>
            </div>

        </div>

    );
}

export default EmployeeSearch;