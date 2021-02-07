api = "http://192.168.2.36:5000";

window.onload = async function() 
{      
    Timer()
    await fetch(api+'/api/Accounts/GetVulcanToken/'+localStorage.getItem("login")).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    var token = `${jsonFile.token}`;

    SendToken(token)
    todayLessons()

    document.getElementById("logininfo").innerHTML = localStorage.getItem("login");
};

function Timer()
{
    var today = new Date();
             
    var hour = today.getHours();
    if (hour<10) hour = "0"+hour;
            
    var minut = today.getMinutes();
    if (minut<10) minut = "0"+minut;

    document.getElementById("timer").innerHTML = hour + ":" + minut + " |"

    setTimeout(Timer, 1000);
}

function Logout()
{
    localStorage.setItem("login", "");
    localStorage.setItem("password", "");
    window.location.href = "index.html"
}