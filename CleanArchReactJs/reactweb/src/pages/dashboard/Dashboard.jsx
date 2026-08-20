import { useEffect, useState } from "react";

import { getDashboard } from "../../api/common/dashboardApi";
import DashboardCard from "../../components/dashboard/DashboardCard";

function Dashboard() {

    const [dashboard, setDashboard] = useState(null);

    useEffect(() => {

        loadDashboard();

    }, []);

    async function loadDashboard() {

        const response = await getDashboard();

        setDashboard(response.data.data);
    }

    if (!dashboard)
        return <div>Loading...</div>;

    return (

        <>

            <DashboardCard
                title="Employees"
                value={dashboard.totalEmployees}
            />

            <DashboardCard
                title="Active"
                value={dashboard.activeEmployees}
            />

            <DashboardCard
                title="Departments"
                value={dashboard.departments}
            />

            <DashboardCard
                title="New This Month"
                value={dashboard.newEmployeesThisMonth}
            />

        </>

    );

}

export default Dashboard;