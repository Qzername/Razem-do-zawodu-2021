api = "http://192.168.2.36:5000";

window.onload = async function() {

    if(localStorage.getItem("login") == "")
        window.location.href = "index.html"

    await fetch(api+'/api/Accounts/GetVulcanToken/'+localStorage.getItem("login")).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );

    if(`${jsonFile.token}` != `${jsonFile.password}`)
        window.location.href = "mainpage.html"
};

async function RegisterToken()
{
    if(document.getElementById("tokeninput").value == "" ||document.getElementById("symbolinput").value==""||document.getElementById("pininput").value=="")
    {
        output.style.color = "red";
        document.getElementById("output").innerHTML = "Wszystkie pola muszą być wypełnione.";
        return;
    }

    var data = { 
        login: localStorage.getItem("login"), 
        password: localStorage.getItem("password"),
        token: document.getElementById("tokeninput").value, 
        symbol: document.getElementById("symbolinput").value,
        pin: document.getElementById("pininput").value 
    };

    await fetch( api + '/api/Accounts/RegisterVulcan/', {
        method: 'POST', headers: {'Content-Type': 'application/json',},body: JSON.stringify(data),}).then(response => response.json()).then(data => {console.log('Success:', data);}).catch((error) => {console.error('Error:', error);});

    window.location.href = "mainpage.html"
}

function Logout()
{
    localStorage.setItem("login", "");
    localStorage.setItem("password", "");
    window.location.href = "index.html"
}