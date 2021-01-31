var login;
var password;

api = "https://localhost:5001";

function Register()
{
    if(document.getElementById("logininput").value == "" ||document.getElementById("passwordinput").value=="")
    {
        document.getElementById("output").innerHTML = "Hasło i login muszą być wypełnione.";
        return;
    }

    var data = { login: document.getElementById("logininput").value, password: document.getElementById("passwordinput").value };

    fetch( api + '/api/Accounts/Register/', {
    method: 'POST', headers: {'Content-Type': 'application/json',},body: JSON.stringify(data),}).then(response => response.json()).then(data => {console.log('Success:', data);}).catch((error) => {console.error('Error:', error);});

    document.getElementById("output").innerHTML = "Zarejestrowano";
}

async function Login()
{
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