const toggleButtonAccess = () => {
    var buttons = document.getElementsByTagName('button');
    for (var i = 0; i < buttons.length; i++) {
        buttons[i].disabled = !buttons[i].disabled;
    }
};

const establishConnection = (connection) => {
    const handleError = (response) => {
        if (!response.ok) {
            throw Error(response.statusText)
        }
    };


    fetch("http://localhost:5000/api/v1/login", {
        method: "POST",
        credentials: "include",
        headers: {"Content-Type" : "application/json; charset=utf-8"},
        body: JSON.stringify({
            Username: "Tolstovku",
            Password: "123"
        })
    })
        .then(handleError)
        .then(response => {
            connection.start();
            toggleButtonAccess();
            console.log("Everythings cool")
        })
        .catch(error => console.log);
};
