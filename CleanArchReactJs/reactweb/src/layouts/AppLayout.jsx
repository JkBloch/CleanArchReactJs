import Header from "../components/layout/Header";
import Sidebar from "../components/layout/Sidebar";
import Footer from "../components/layout/Footer";

import { Outlet } from "react-router-dom";

function AppLayout() {

    return (

        <div>

            <Header />

            <div className="d-flex">

                <Sidebar />

                <div
                    className="flex-grow-1 bg-light"
                    style={{
                        minHeight: "100vh"
                    }}
                >

                    <div className="p-4">

                        <Outlet />

                    </div>

                    <Footer />

                </div>

            </div>

        </div>

    );

}

export default AppLayout;