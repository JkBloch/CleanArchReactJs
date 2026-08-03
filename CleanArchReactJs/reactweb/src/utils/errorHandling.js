export function getErrorMessage(error) {
    console.log(error);
    if (!error.response)
        return "Server unavailable";
    if (error.response.data.message) {
        return error.response.data.message;
    }  

    if (error.response?.status === 400) {
        
        const errors = error.response.data.errors;

        if (errors) {
            var serror=""
            Object.keys(errors).forEach(key => {
                serror = serror + "," + errors[key][0];
            });
            return serror;
        }
    } 

    if (error.response.data.errors)
        return error.response.data.errors;

    if (error.response.data.title)
        return error.response.data.title;

    return error.response.data;
}