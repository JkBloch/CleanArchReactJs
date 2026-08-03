import { useEffect, useState } from "react";

function EmployeeSearch({ filters, onSearch }) {

    const [form, setForm] = useState(filters);

    useEffect(() => {
        const timer = setTimeout(() => {
            onSearch(form);
        }, 500);

        return () => clearTimeout(timer);
    }, [form]);

    function handleChange(e) {

        const { name, value } = e.target;

        setForm(prev => ({
            ...prev,
            [name]: value,
            pageNumber: 1
        }));
    }

    return (

        <div className="card mb-3">

            <div className="card-body">

                <div className="row">

                    <div className="col-md-4">

                        <input
                            className="form-control"
                            name="keyword"
                            placeholder="Search employee..."
                            value={form.keyword}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-3">

                        <input
                            className="form-control"
                            name="department"
                            placeholder="Department"
                            value={form.department}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-2">

                        <input
                            type="number"
                            className="form-control"
                            placeholder="Min Salary"
                            name="minSalary"
                            value={form.minSalary ?? ""}
                            onChange={handleChange}
                        />

                    </div>

                    <div className="col-md-2">

                        <input
                            type="number"
                            className="form-control"
                            placeholder="Max Salary"
                            name="maxSalary"
                            value={form.maxSalary ?? ""}
                            onChange={handleChange}
                        />

                    </div>

                </div>

            </div>

        </div>

    );
}

export default EmployeeSearch;