import IconButton from "../../components/common/IconButton";

function Dashboard() {
    return (
      <>
       <div className="d-flex flex-wrap gap-3">

            <IconButton
                color="success"
                icon="bi-plus-lg"
                text="Add"
            />

            <IconButton
                color="primary"
                icon="bi-floppy"
                text="Save"
            />

            <IconButton
                color="warning"
                icon="bi-pencil-square"
                text="Edit"
            />

            <IconButton
                color="danger"
                icon="bi-trash"
                text="Delete"
            />

            <IconButton
                color="info"
                icon="bi-search"
                text="Search"
            />

            </div>
            <h1>This is dash board design is pending!</h1>
          
      </>
    
  );
}

export default Dashboard;