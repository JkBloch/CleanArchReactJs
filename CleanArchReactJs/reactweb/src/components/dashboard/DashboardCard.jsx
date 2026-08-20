import React from "react";

function DashboardCard({
    title,
    value,
    icon,
    color
}) {
    //<div
    //    className={`card border-${color} shadow`}
    //    style={{ cursor: "pointer" }}
    //    onClick={onClick}>
    return (

        <div className="col-lg-3 col-md-6 mb-3">

            <div className={`card border-${color} shadow`}>

                <div className="card-body">

                    <div className="d-flex justify-content-between">

                        <div>

                            <h6>{title}</h6>

                            <h3>{value}</h3>

                        </div>

                        <div>

                            <i
                                className={`${icon} fs-1 text-${color}`}
                            ></i>

                        </div>

                    </div>

                </div>

            </div>

        </div>

    );
}

export default DashboardCard;