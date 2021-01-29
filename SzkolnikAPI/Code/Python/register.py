from vulcan import Vulcan
import json
import sys

certificate = Vulcan.register(sys.argv[1], sys.argv[2], sys.argv[3])

with open('./'+sys.argv[1]+'.json', 'w') as f: # You can use other filename
    json.dump(certificate.json, f)