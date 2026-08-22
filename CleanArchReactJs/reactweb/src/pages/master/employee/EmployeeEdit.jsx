import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import EmployeeForm from "../employee/EmployeeForm";
import { getEmployee, updateEmployee } from "../.././../api/master/employeeApi";
import { notify } from "../../../services/notificationService";

function EmployeeEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [employee, setEmployee] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadEmployee();

    }, []);

    async function loadEmployee() {

        const response = await getEmployee(id);

        setEmployee(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateEmployee(id, data);
            notify.success(

                "Employee updated successfully."

            );


            navigate("/employees");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit Employee</h2>

            <EmployeeForm
                initialValues={employee}
                onSubmit={save}
                loading={loading}
                loadEmployee={loadEmployee}
            />

        </div>

    );
}

export default EmployeeEdit;