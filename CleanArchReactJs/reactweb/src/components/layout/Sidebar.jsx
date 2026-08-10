import {NavLink} from "react-router-dom";

import {
    FaListAlt, FaHome, FaUsers, FaBuilding, FaChartBar,
    FaUserShield, FaUserCog, FaHistory, FaCog} from "react-icons/fa";

const menus = [
    {
        name: "Dashboard",
        icon: <FaHome />,
        path: "/dashboard"
    },
    {
        name: "Permission",
        icon: <FaListAlt />,
        path: "/permissions"
    },
    {
        name: "Roles",
        icon: <FaUserShield />,
        path: "/roles"
    },
    {
        name: "RolePermissions",
        icon: <FaUserShield />,
        path: "/rolePermissions"
    },
    {
        name: "Users",
        icon: <FaUserCog />,
        path: "/users"
    },

    {
        name: "Employees",
        icon: <FaUsers />,
        path: "/employees"
    },

    {
        name: "Departments",
        icon: <FaBuilding />,
        path: "/departments"
    },

    {
        name: "Reports",
        icon: <FaChartBar />,
        path: "/reports"
    },



  

    {
        name: "Audit Logs",
        icon: <FaHistory />,
        path: "/audit"

    },

    {
        name: "Settings",
        icon: <FaCog />,
        path: "/settings"

    }

];

function Sidebar() {

    return (

        <div
            className="bg-dark text-white vh-100"
            style={{

                width: 250

            }}
        >

            <div className="p-3">

                <h5>

                    MENU

                </h5>

            </div>

            {

                menus.map(menu => (

                    <NavLink

                        key={menu.path}

                        to={menu.path}

                        className={({ isActive }) =>
                            `d-flex align-items-center text-decoration-none px-3 py-2 ${isActive
                                ? "bg-primary text-white"
                                : "text-light"
                            }`
                        }

                    >

                        <span className="me-3">

                            {menu.icon}

                        </span>

                        {menu.name}

                    </NavLink>

                ))

            }

        </div>

    );

}

export default Sidebar;