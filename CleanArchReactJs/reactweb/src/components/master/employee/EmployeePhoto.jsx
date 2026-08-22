import { useState } from "react";
import { getImageUrl } from "../../../utils/imageHelper";

function EmployeePhoto({
    photoUrl,
    firstName = "",
    lastName = "",
    size = 80
}) {

    const [imageError, setImageError] =
        useState(false);

    const imageUrl =
        getImageUrl(photoUrl);

    const initials =
        `${firstName?.charAt(0) || ""}${lastName?.charAt(0) || ""}`
            .toUpperCase();

    if (!imageUrl || imageError) {

        return (
            <div
                className="
                    rounded-circle
                    bg-secondary
                    text-white
                    d-flex
                    align-items-center
                    justify-content-center
                "
                style={{
                    width: size,
                    height: size,
                    fontSize: size * 0.35,
                    fontWeight: "bold"
                }}
            >
                {initials || "?"}
            </div>
        );
    }

    return (
        <img
            src={imageUrl}
            alt={`${firstName} ${lastName}`}
            className="rounded-circle border"
            style={{
                width: size,
                height: size,
                objectFit: "cover"
            }}
            onError={() => setImageError(true)}
        />
    );
}

export default EmployeePhoto;