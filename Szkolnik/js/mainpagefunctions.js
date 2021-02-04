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
    document.getElementById("contentjs").innerHTML = '<div class="center-text"> Wczytywanie... </div>';

    var today = new Date();

    var pon = await ReturnLesson(2021, 02, 01)
    var wto = await ReturnLesson(2021, 02, 02)
    var sro = await ReturnLesson(2021, 02, 03)
    var czw = await ReturnLesson(2021, 02, 04)
    var pia = await ReturnLesson(2021, 02, 05)

    var html = '<div class="center-text" style="width:100%; height: 100%;"> <div class="day">Poniedziałek'+pon+'</div> <div class="day">Wtorek'+wto+'</div> <div class="day">Środa'+sro+'</div> <div class="day">Czwartek'+czw+'</div> <div class="day">Piątek'+pia+'</div> </div>'
    
    document.getElementById("contentjs").innerHTML = html;
}

async function ReturnLesson(year, month, day)
{
    await fetch(api+'/api/Vulcan/GetLessons/'+token+"/"+year+"/"+month+"/"+day).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    
    var lessons = ""

    for(i = 0; i<jsonFile.length; i++)
    {
        var from = jsonFile[i].fromTime;
        var to =jsonFile[i].toTime;

        var newFrom = jsonFile[i].fromTime.charAt(11) + jsonFile[i].fromTime.charAt(12) +jsonFile[i].fromTime.charAt(13) + jsonFile[i].fromTime.charAt(14) + jsonFile[i].fromTime.charAt(15)
        var newTo = jsonFile[i].toTime.charAt(11) + jsonFile[i].toTime.charAt(12) +jsonFile[i].toTime.charAt(13) + jsonFile[i].toTime.charAt(14) + jsonFile[i].toTime.charAt(15)
        
        jsonFile[i].fromTime = newFrom;
        jsonFile[i].toTime= newTo
    }

    lessons += ('<br>-------------------<br>');
    jsonFile.forEach(obj => {
        lessons += obj.subject + "<br>Od: " +  obj.fromTime + " Do: "+ obj.toTime;
        lessons += ('<br>-------------------<br>');
    });

    return lessons
}

async function Marks()
{
    document.getElementById("contenttitle").innerHTML = "Oceny";
    document.getElementById("contentjs").innerHTML = '<div class="center-text"> Wczytywanie... </div>';
    
    await fetch(api+'/api/Vulcan/GetMarks/'+token ).then( res => res.json() ).then( json => jsonFile = json ).catch( e => console.log(e) );
    
    var oceny = ""

    var Subjects = []
    var isInTable = false;

    jsonFile.forEach(obj => 
    {
        for(i = 0; i<Subjects.length;i++)
            if(Subjects[i].split('|')[0] == obj.subject)
            {
                Subjects[i] += " " + obj.content
                isInTable = true;
            }

        if(!isInTable)
            Subjects.push((obj.subject +"|"+obj.content));

        isInTable = false;
    });

    var html = ""

    Subjects.forEach(obj => 
    {
        splited = obj.split('|')
        html += '<br><div class="marktitle">'+ splited[0] +":</div>"
         
        marks = splited[1].split(' ')
        marks.forEach(mark => {
            html += '<div class="mark">'+mark+'</div>'
        });
    });

    document.getElementById("contentjs").innerHTML = html;
}