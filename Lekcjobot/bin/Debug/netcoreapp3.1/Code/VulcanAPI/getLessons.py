from vulcan import Vulcan;
import datetime
import json
import sys
# -*- coding: utf-8 -*-

utf8stdout = open(1, 'w', encoding='utf-8', closefd=False) # fd 1 is stdout

with open('./cert.json') as f:
    certificate = json.load(f);

client = Vulcan(certificate);

for student in client.get_students():
    if student.name == "Hubert Szymik":
        client.set_student(student);
        break;

# rok miesiac dzien 
day = datetime.date(int(sys.argv[1]), int(sys.argv[2]), int(sys.argv[3]))

final = "";

for lesson in client.get_lessons(day, day):
    final += lesson.teacher.name + "!" + lesson.subject.name + "!" + lesson.from_.strftime("%H:%M:%S") + "!" + lesson.to.strftime("%H:%M:%S") + "|";

print(final, file=utf8stdout);