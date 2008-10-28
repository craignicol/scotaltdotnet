#!/usr/bin/env python
from __future__ import with_statement
import re

class C():
    """
    >>> c = C("football.dat")
    >>> c.filename
    'football.dat'
    """
    def __init__(self, filename):
        self.filename = filename

    def read(self):
        """
        >>> c = C("football.dat")       
        >>> c.read()
        >>> c.lines is not None
        True
        """
        with open(self.filename) as f:
            self.lines = f.readlines()
        

    def match(self, line):
        pattern = r"\s+\d{1,2}\.\s([A-Za-z]{4,}).+\s(\d{1,2})[^\d]+-[^\d]+(\d{1,2})\s"
        return re.match(pattern, line)


    def matchline(self, line):
        """
        >>> c = C("")
        >>> line = '    1. Arsenal         38    26   9   3    79  -  36    87'
        >>> c.matchline(line) is not None
        True
        >>> "Arsenal" in c.matchline(line)
        True
        >>> c.matchline(line)
        ('Arsenal', '79', '36')
        """
        return self.match(line).groups()

    def getDifference(self, line):
        """
        >>> c = C("")
        >>> line = '    1. Arsenal         38    26   9   3    79  -  36    87'
        >>> c.getDifference(line)
        43
        """
        (team, goalsfor, goalsagainst) = self.matchline(line)
        return abs(int(goalsfor) - int(goalsagainst))

    def getTeamDifference(self, line):
        """
        >>> c = C("")
        >>> line = '    1. Arsenal         38    26   9   3    79  -  36    87'
        >>> c.getTeamDifference(line)
        ('Arsenal', 43)
        """
        (team, goalsfor, goalsagainst) = self.matchline(line)
        return team, abs(int(goalsfor) - int(goalsagainst))
        
    def getSmallestTeamDifference(self):
        """
        >>> c = C("football.dat")
        >>> c.read()
        >>> c.getSmallestTeamDifference()
        'Aston'
        """
        return min([self.getTeamDifference(line) for line in self.lines if self.match(line) is not None], key=(lambda x: x[1]))[0]

def _test():
    import doctest
    doctest.testmod()

if __name__ == "__main__":
    _test()
