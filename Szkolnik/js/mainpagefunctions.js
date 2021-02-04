api = "https://localhost:5001";
var token = ""

function SendToken(tokenbrrr)
{
    token = tokenbrrr
}

async function todayLessons()
{

    var today = new Date();

    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();

    await fetch(api+'/api/Vulcan/GetLessons/'+token+"/"+yyyy+"/"+mm+"/"+dd).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );

    var lessons = ""

    for(i = 0; i<jsonFile.length; i++)
    {
        var from = jsonFile[i].fromTime;
        var to =jsonFile[i].toTime;

        var newFrom = jsonFile[i].fromTime.charAt(11) + jsonFile[i].fromTime.charAt(12) +jsonFile[i].fromTime.charAt(13) + jsonFile[i].fromTime.charAt(14) + jsonFile[i].fromTime.charAt(15)
        var newTo = jsonFile[i].toTime.charAt(11) + jsonFile[i].toTime.charAt(12) +jsonFile[i].toTime.charAt(13) + jsonFile[i].toTime.charAt(14) + jsonFile[i].toTime.charAt(15)
        
        jsonFile[i].fromTime = newFrom;
        jsonFile[i].toTime= newTo


        console.log(jsonFile[i].fromTime);
    }

    lessons += ('-------------------<br>');
    jsonFile.forEach(obj => {
        lessons += obj.subject + "<br>Od: " +  obj.fromTime + " Do: "+ obj.toTime;
        lessons += ('<br>-------------------<br>');
    });

    document.getElementById("todaylessonscontent").innerHTML = lessons;
}

async function Lessons()
{
    document.getElementById("contenttitle").innerHTML = "Plan lekcji";

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
    document.getElementById("contenttitle").innerHTML = "Oceny";


    await fetch(api+'/api/Vulcan/GetMarks/'+token ).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    
    var oceny = ""

    oceny += ('-------------------<br>');
    jsonFile.forEach(obj => {
        oceny += "Ocena: " + obj.content + "<br>Nauczyciel: " +obj.teacher + "<br>Przedmiot: " + obj.subject + "<br>";
        oceny += ('-------------------<br>');
    });

    document.getElementById("output").innerHTML = oceny;
}