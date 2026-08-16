import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createEmployee } from "../.././../api/master/employeeApi";
import EmployeeForm from "../employee/EmployeeForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function EmployeeCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: "",
        email: "",  
        gender:0,
        isActive:true
    });

    async function save(employee) {

        setLoading(true);

        try {

            await createEmployee(employee);
            notify.success(
                "Employee created successfully."
            );

            //   navigate("/employees");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setInitialValues(employee);

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