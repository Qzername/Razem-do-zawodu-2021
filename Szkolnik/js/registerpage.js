var login;
var password;

api = "http://192.168.2.36:5000";

function Register()
{
    if(document.getElementById("logininput").value == "" ||document.getElementById("passwordinput").value=="")
    {
        output = document.getElementById("output");
        output.innerHTML = "Hasło i login muszą być wypełnione.";
        output.style.color = "red";
        return;
    }

    var data = { login: document.getElementById("logininput").value, password: document.getElementById("passwordinput").value };

    fetch( api + '/api/Accounts/Register/', {
    method: 'POST', headers: {'Content-Type': 'application/json',},body: JSON.stringify(data),}).then(response => response.json()).then(data => {console.log('Success:', data);}).catch((error) => {console.error('Error:', error);});

    output = document.getElementById("output");
    output.innerHTML = "Zarejestrowano";
    output.style.color = "green";
}

async function Login()
{
    output.style.color = "red";

    if(document.getElementById("logininput").value == "" ||document.getElementById("passwordinput").value=="")
    {
        document.getElementById("output").innerHTML = "Hasło i login muszą być wypełnione.";
        return;
    }

    await fetch(api+'/api/Accounts/Login/'+document.getElementById("logininput").value+'/' + document.getElementById("passwordinput").value ).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );

    if(`${jsonFile}` == "null")
    {
        document.getElementById("output").innerHTML = "Błędne hasło i login";
        return;
    }

    localStorage.setItem("login", `${jsonFile.login}`);
    localStorage.setItem("password", `${jsonFile.password}`);

    window.location.href = "registertoken.html"
}