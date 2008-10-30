#!/usr/bin/env python
from __future__ import with_statement

with open("weather.dat") as f:
    print dir(f)
    resultday, resultrange = (0, 1000)
    for l in f.readlines():
        try:
            day, maxT, minT = l.split()[0:3]
            day, range = int(day), int(maxT)-int(minT)
            if range < resultrange:
                resultday, resultrange = day, range
        except:
            pass # Ignore errors

    if resultrange < 1000:
        print "Minimum range of", resultrange, "was on day", resultday
    else:
        print "No valid weather data found"
