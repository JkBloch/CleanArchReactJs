import {

    createContext,

    useContext,

    useState

} from "react";

const LoadingContext = createContext();

export function LoadingProvider({

    children

}) {

    const [loading, setLoading] = useState(false);

    return (

        <LoadingContext.Provider

            value={{

                loading,

                showLoading: () => setLoading(true),

                hideLoading: () => setLoading(false)

            }}

        >

            {children}

        </LoadingContext.Provider>

    );

}

export const useLoading = () =>
    useContext(LoadingContext);