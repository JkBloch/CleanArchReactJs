import { useNavigate } from "react-router-dom";
import { useState } from "react";
import { createDepartment } from "../.././../api/master/departmentApi";
import DepartmentForm from "../department/DepartmentForm";
import { notify } from "../../../services/notificationService";
//import { getErrorMessage } from "../../../utils/errorHandling";

function DepartmentCreate() {

    const navigate = useNavigate();

    const [loading, setLoading] = useState(false);
    const [initialValues, setInitialValues] = useState({
        code: "",
        name: ""
    });

    async function save(department) {

        setLoading(true);

        try {

            await createDepartment(department);
            notify.success(
                "Department created successfully."
            );

            //   navigate("/departments");

        }
        catch (error) {
            //notify.error(getErrorMessage(error));
            setInitialValues(department);

        }
        finally {

            setLoading(false);

        }

    }

    return (

        <div className="container">

            <h2>Add Department</h2>

            <DepartmentForm
                initialValues={initialValues}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default DepartmentCreate;