function Pagination({

    pageNumber,
    totalPages,
    onPageChange

}) {

    if (totalPages <= 1)
        return null;

    const pages = [];

    for (let i = 1; i <= totalPages; i++) {

        pages.push(

            <button
                key={i}
                className={
                    i === pageNumber
                        ? "btn btn-primary me-2"
                        : "btn btn-outline-primary me-2"
                }
                onClick={() => onPageChange(i)}
            >
                {i}
            </button>

        );

    }

    return (

        <div className="mt-3">

            {pages}

        </div>

    );
}

export default Pagination;