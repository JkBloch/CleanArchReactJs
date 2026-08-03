import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { getEmployees, deleteEmployee } from "../../api/employeeApi";
import Loader from "../../components/Loader";
import EmployeeTable from "../../components/employee/EmployeeTable"; 
import EmployeeSearch from "../employee/EmployeeSearch";
import Pagination from "../../../components/common/Pagination";
import { searchEmployees } from "../../../api/employeeApi";
function EmployeeList() {

    const [employees, setEmployees] = useState([]);

    const [totalPages, setTotalPages] = useState(0);

    const [filters, setFilters] = useState({

        keyword: "",
        department: "",
        minSalary: "",
        maxSalary: "",

        pageNumber: 1,
        pageSize: 10,

        sortBy: "firstName",
        descending: false
    });

    useEffect(() => {

        loadEmployees();

    }, [filters]);

    async function loadEmployees() {

        const response = await searchEmployees(filters);

        const result = response.data.data;

        setEmployees(result.items);

        setTotalPages(result.totalPages);
    }

    function handleSearch(newFilters) {

        setFilters(newFilters);
    }

    function changePage(page) {

        setFilters(prev => ({
            ...prev,
            pageNumber: page
        }));
    }

    function sort(column) {

        setFilters(prev => ({

            ...prev,

            sortBy: column,

            descending:
                prev.sortBy === column
                    ? !prev.descending
                    : false
        }));
    }

    return (

        <>

            <EmployeeSearch
                filters={filters}
                onSearch={handleSearch}
            />

            <EmployeeTable
                employees={employees}
                onSort={sort}
            />

            <Pagination
                pageNumber={filters.pageNumber}
                totalPages={totalPages}
                onPageChange={changePage}
            />

        </>

    );
}

export default EmployeeList;