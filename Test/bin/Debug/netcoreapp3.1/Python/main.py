from vulcan import Vulcan;
import json;

with open('./cert.json') as f:
    certificate = json.load(f);

client = Vulcan(certificate);

for student in client.get_students():
    if student.name == "Hubert Szymik":
        client.set_student(student);
        break;

final = "";

for grade in client.get_grades():
    final += grade.teacher.name + " " + grade.subject.name + " " + grade.content + "\n";


print(final);