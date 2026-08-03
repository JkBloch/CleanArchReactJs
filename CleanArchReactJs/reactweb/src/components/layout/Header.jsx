//import useAuth from "../hooks/useAuth";

function Header() {

    //const { logout, user } = useAuth();

    return (

        <nav className="navbar navbar-dark bg-dark">

            <div className="container">

                <span className="navbar-brand">
                Welcome
                    {/*Welcome {user?.firstName}*/}

                </span>

                {/*<button*/}
                {/*    className="btn btn-danger"*/}
                {/*    onClick={logout}*/}
                {/*>*/}
                {/*    Logout*/}
                {/*</button>*/}

            </div>

        </nav>

    );
}

export default Header;