import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import App from './App.jsx'
import "bootstrap/dist/css/bootstrap.min.css";
import 'bootstrap-icons/font/bootstrap-icons.css';
import '../src/assets/button.css';
import { ToastContainer } from "react-toastify";
import '../src/styles/mystyle.css'

createRoot(document.getElementById('root')).render(    
    /*StrictMode*/
    <>
        <App />
        <ToastContainer
            position="top-right"
            autoClose={3000}
            newestOnTop
            pauseOnHover
            closeOnClick />
    </ > 

  ,
)
