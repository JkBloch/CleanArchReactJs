import { BrowserRouter } from "react-router-dom";

import { AuthProvider } from "./context/AuthContext";

import AppRoutes from "./routes/AppRoutes";
import { LoadingProvider } from "./context/LoadingContext";
import LoadingOverlay from "./components/common/LoadingOverlay";

function App() {
    return (
        <BrowserRouter>
            <AuthProvider>
                <LoadingProvider>
                    <LoadingOverlay>
                        <AppRoutes />
                    </LoadingOverlay>
                </LoadingProvider>
            </AuthProvider>
        </BrowserRouter>

    );
}

export default App;