
//get Register info
function getInfo() {
    var names = document.getElementById("names");
    var lastname = document.getElementById("lastnames");
    var email = document.getElementById("email");
    var passwd = document.getElementById("psswd");
    var confirmPasswd = document.getElementById("confirmpsswd");
    var username = document.getElementById("username");
    var xmlClient = new XMLHttpRequest();
    
    xmlClient.open("POST", "/Account/Register");
    xmlClient.setRequestHeader("content-type", "application/json")
    xmlClient.send(new {
        "FirstName" : names,
        "LastName" : lastname,
        "Email" : email,
        "ConfirmPassword" : confirmPasswd,
        "Password" : passwd,
        "Username" : username

    })

};

function getLogIn () {
    var username = document.getElementById("name");
    var passwd = document.getElementById("passwd");

    xmlClient.open("POST", "/Account/Login");
    xmlClient.setRequestHeader("content-type", "application/json")
    xmlClient.send(new {
        "Password" : passwd,
        "Username" : username

    })
    
};



