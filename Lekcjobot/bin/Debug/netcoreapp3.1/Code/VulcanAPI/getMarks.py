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

final = ""

for grade in client.get_grades():
    final += grade.content + "!" + grade.subject.name + "!" + grade.teacher.name + "|";

print(final, file=utf8stdout);