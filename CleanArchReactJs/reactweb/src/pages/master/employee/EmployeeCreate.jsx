import { useNavigate } from "react-router-dom";
import { useState } from "react";

import EmployeeForm from "./EmployeeForm";
import { createEmployee } from "../../api/employeeApi";
import { notify } from "../../services/notificationService";

function EmployeeCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);

    const initialValues = {

        employeeCode: "EMp001",
        firstName: "Javed",
        lastName: "Bloch",
        email: "blochjaved@gmail.com",
        phoneNumber: "1234567890",
        department: "IT",
        salary: 25000,
        joiningDate: "2024-03-31",
        dateOfBirth: "1998-05-20",
        gender: 1
    };
    function formatDateForInput(date) {
        if (!date) return "";

        const d = new Date(date);

        const year = d.getFullYear();
        const month = String(d.getMonth() + 1).padStart(2, "0");
        const day = String(d.getDate()).padStart(2, "0");

        return `${year}-${month}-${day}`;
    }
    async function save(employee) {
        //setEmployee({
        //    ...employee,
        //    dateOfBirth: formatDateForInput(employee.dateOfBirth,
        //        joiningDate: formatDateForInput(employee.joiningDate)
        //});
        setLoading(true);

        try {

            await createEmployee(employee);
            notify.success(

                "Employee created successfully."

            );

            navigate("/employees");

        }
        catch (error) {

            alert(
                error.response?.data?.message ??
                "Unable to create employee."
            );

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add Employee</h2>

            <EmployeeForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default EmployeeCreate;