import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import DepartmentForm from "../department/DepartmentForm";
import { getDepartment, updateDepartment } from "../.././../api/master/departmentApi";
import { notify } from "../../../services/notificationService";

function DepartmentEdit() {

    const { id } = useParams();

    const navigate = useNavigate();

    const [department, setDepartment] = useState(null);

    const [loading, setLoading] = useState(true);

    useEffect(() => {

        loadDepartment();

    }, []);

    async function loadDepartment() {

        const response = await getDepartment(id);

        setDepartment(response.data.data);

        setLoading(false);
    }

    async function save(data) {

        setLoading(true);

        try {

            await updateDepartment(id, data);
            notify.success(

                "Department updated successfully."

            );


            navigate("/departments");

        }
        finally {

            setLoading(false);

        }

    }

    if (loading)
        return <div>Loading...</div>;

    return (

        <div className="container">

            <h2>Edit Department</h2>

            <DepartmentForm
                initialValues={department}
                onSubmit={save}
                loading={loading}
            />

        </div>

    );
}

export default DepartmentEdit;