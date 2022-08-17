export const requestMethods = {
    GET: 'GET',
    POST: 'POST'
};

export const sendRequest = (api, requestMethod, payload) => {
    return fetch(`https://bringsallyup20220812231522.azurewebsites.net/api${api}`, {
    // return fetch(`https://localhost:44377/api${api}`, {
        method: requestMethod,
        body: payload && JSON.stringify(payload),
        headers: {
            "Content-Type": "application/json"
        }
    });
}