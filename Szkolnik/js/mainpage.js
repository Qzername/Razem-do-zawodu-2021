api = "https://localhost:5001";
var token = "";

window.onload = async function() {

    await fetch(api+'/api/Accounts/GetVulcanToken/'+localStorage.getItem("login")).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    token = `${jsonFile.token}`;

};

function Logout()
{
    localStorage.setItem("login", "");
    localStorage.setItem("password", "");
    window.location.href = "index.html"
}

async function Lessons()
{
    var today = new Date();

    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    await fetch(api+'/api/Vulcan/GetLessons/'+token+"/"+yyyy+"/"+mm+"/"+dd).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    
    var lessons = ""

    lessons += ('-------------------<br>');
    jsonFile.forEach(obj => {
        lessons += "Nauczyciel: " +obj.teacher + "<br>Przedmiot: " + obj.subject + "<br>Od: " +  obj.fromTime + "<br>Do: "+ obj.toTime;
        lessons += ('<br>-------------------<br>');
    });

    document.getElementById("output").innerHTML = lessons;
}

async function Marks()
{
    await fetch(api+'/api/Vulcan/GetMarks/'+token ).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    
    var oceny = ""

    oceny += ('-------------------<br>');
    jsonFile.forEach(obj => {
        oceny += "Ocena: " + obj.content + "<br>Nauczyciel: " +obj.teacher + "<br>Przedmiot: " + obj.subject + "<br>";
        oceny += ('-------------------<br>');
    });

    document.getElementById("output").innerHTML = oceny;
}