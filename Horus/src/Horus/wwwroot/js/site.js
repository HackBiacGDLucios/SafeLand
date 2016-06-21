
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
            alert("An error ocurred");
        }
    }
}

function getChildInfo(){
    var namesChild = document.getElementById("names").value;
    var lastNameChild = document.getElementById("lastnames").value;
    var emailChild = document.getElementById("email").value;
    var passwdChild = document.getElementById("psswd").value;
    var xmlClient = new XMLHttpRequest();

    xmlClient.open("POST", "/Parent/AddChild");
    xmlClient.setRequestHeader("Content-Type", "application/json");
    xmlClient.send(
        JSON.stringify({
        "FirstName": namesChild,
        "LastName": lastNameChild,
        "Email": emailChild,
        "Password": passwdChild,
        "ParentId": localStorage.getItem("Id"),
    }));
    xmlClient.onload = function () {
        try {
            var id = xmlClient.response.Id;
            localStorage.setItem("RecentKid", id);
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
            localStorage.setItem("isChild", true)
        } catch (e) {
            alert("An error ocurred");
        }
    }
}

function PostAlert(type) {
    var message = document.getElementById("message").value;

    xmlClient = new XMLHttpRequest();
    xmlClient.open("POST", "/Alert/PushAlert");
    xmlClient.setRequestHeader("Content-Type", "application/json");
    xmlClient.responseType = 'json';
    
    var lat = 1.0;
    var lon = 1.0;

    navigator.geolocation.getCurrentPosition(function (position) {
        lat = position.coords.latitud;
        lon = position.coords.longitude;
    });

    xmlClient.send(
        JSON.stringify({
            "IsChild": localStorage.getItem("isChild"),
            "Type": type,
            "Message": message,
            "UserId": localStorage.getItem("Id"),
            "Location": {
                "Lat": lat,
                "Lon": lon
            }
        }));

    xmlClient.onload = function () {
        try {
            var id = xmlClient.response.Id;
            console.log(id);
            localStorage.setItem("LastAlert", id);
        } catch (e) {
            alert("An error ocurred");
        }
    }
}

function ParentLogin() {
    var username = document.getElementById("username").value;
    var passwd = document.getElementById("passwd").value;

    xmlClient = new XMLHttpRequest();

    xmlClient.open("POST", "/Parent/Login");
    xmlClient.setRequestHeader("content-type", "application/json");
    xmlClient.responseType = 'json'
    xmlClient.send(
        JSON.stringify({
            "Password": passwd,
            "Username": username
        }));

    xmlClient.onload = function () {
        try {
            var id = xmlClient.response.Id;
            console.log(id);
            localStorage.setItem("Id", id);
            localStorage.setItem("isChild", true)
        } catch (e) {
            alert("An error ocurred");
        }
    }
}


