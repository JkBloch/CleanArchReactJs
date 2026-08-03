import React from "react";

const IconButton = ({
    text,
    icon,
    color = "primary",
    onClick,
    type = "button"
}) => {

    return (
        <button
            type={type}
            className={`icon-btn icon-btn-${color}`}
            onClick={onClick}
        >
            <span className="icon-section">
                <i className={`bi ${icon}`}></i>
            </span>

            <span className="text-section">
                {text}
            </span>
        </button>
    );
};

export default IconButton;