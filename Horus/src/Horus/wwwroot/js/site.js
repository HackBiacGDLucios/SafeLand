
//get Register info
function getInfo() {
    var names = document.getElementById("names").value;
    var lastname = document.getElementById("lastnames").value;
    var email = document.getElementById("email").value;
    var passwd = document.getElementById("psswd").value;
    var confirmPasswd = document.getElementById("confirmPasswd").value;
    var username = document.getElementById("username").value;

    var xmlClient = new XMLHttpRequest();
    xmlClient.open("POST", "/Account/Register");
    xmlClient.setRequestHeader("Content-Type", "application/json");
    xmlClient.responseType = 'json';
    xmlClient.send(
        JSON.stringify({
        "FirstName": names,
        "LastName": lastname,
        "Email": email,
        "ConfirmPassword": confirmPasswd,
        "Password": passwd,
        "Username": username
        }));

    xmlClient.onload = function () {
        try {
            
            var id = xmlClient.response.Id;
            console.log(id);
            localStorage.setItem("Id", id);
            
        } catch (e) {
            alert("An Error ocurred");
        }
    }
}

function getLogIn() {
    var username = document.getElementById("username").value;
    var passwd = document.getElementById("passwd").value;

    xmlClient = new XMLHttpRequest();

    xmlClient.open("POST", "/Account/Login");
    xmlClient.setRequestHeader("content-type", "application/json");
    xmlClient.responseType='json'
    xmlClient.send(
        JSON.stringify({
        "Password": passwd,
        "Username": username
    }));
    
    xmlClient.onload = function () {
        try{
            var id = xmlClient.response.Id;
            console.log(id);
            localStorage.setItem("Id", id);
        } catch (e) {
            alert("An error ocurred");
        }
    }
}



