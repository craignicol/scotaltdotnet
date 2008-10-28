#!/usr/bin/env python
from __future__ import with_statement

with open("football.dat") as f:
    resultteam, resultrange = ("No Team", 1000)
    Fidx, Aidx = None, None
    for l in f.readlines():
        try:
            cols = l.split()
            print cols, (resultteam, resultrange, Fidx, Aidx)
            if cols[0] == "Team":
                Fidx = cols.index("F") + 1
                Aidx = cols.index("A") + 2
                print "Fidx = ",Fidx,"; Aidx = ",Aidx
            elif (Fidx > 0) and (Aidx > 0):
                team, maxT, minT = cols[1], cols[Fidx], cols[Aidx]
                range = abs(int(maxT)-int(minT))
                print team, maxT, minT, range, resultteam, resultrange
                if range < resultrange:
                    resultteam, resultrange = team, range
        except:
            pass # Ignore errors

    if resultrange < 1000:
        print "Minimum goal difference of", resultrange, "for team", resultteam
    else:
        print "No valid football data found"
