
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
    xmlClient.setRequestHeader("Content-Type", "application/json")
    xmlClient.onreadystatechange = ReadBody();
    xmlClient.send(
        JSON.stringify({
        "FirstName": names,
        "LastName": lastname,
        "Email": email,
        "ConfirmPassword": confirmPasswd,
        "Password": passwd,
        "Username": username
    }));

function getChildInfo(){
    var namesChild = document.getElementById("namesChild").value;
    var lastNameChild = document.getElementById("lastNameChild").value;
    var emailChild = document.getElementById("emailChild").value;
    var passwdChild = document.getElementById("passwdChild").value;
    var xmlClinet = new XMLHttpRequest();

    xmlClient.open("POST", "/Account/Register");
    xmlClient.setRequestHeader("Content-Type", "application/json")
    xmlClient.onreadystatechange = ReadBody();
    xmlClient.send(
        JSON.stringify({
        "FirstNameChild": namesChild,
        "LastNameChild": lastNameChild,
        "EmailChild": emailChild,
        "PasswordChild": passwdChild,
        "ParentId": localStorage.getItem("Id"),
    }));
}

    function ReadBody() {
        var id = xmlClient.responseBody;
        console.debug("Body", id);
    }
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



