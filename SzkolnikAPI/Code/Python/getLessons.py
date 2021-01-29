from vulcan import Vulcan;
import datetime
import json
import sys
# -*- coding: utf-8 -*-

utf8stdout = open(1, 'w', encoding='utf-8', closefd=False) # fd 1 is stdout

with open('./'+sys.argv[1]+'.json') as f:
    certificate = json.load(f);

client = Vulcan(certificate);

table = client.get_students()
client.set_student(next(table))

# rok miesiac dzien 
day = datetime.date(int(sys.argv[2]), int(sys.argv[3]), int(sys.argv[4]))

final = "";

for lesson in client.get_lessons(day, day):
    final += lesson.teacher.name + "!" + lesson.subject.name + "!" + lesson.from_.strftime("%Y/%m/%d %H:%M:%S") + "!" + lesson.to.strftime("%Y/%m/%d %H:%M:%S") + "|";

print(final, file=utf8stdout);