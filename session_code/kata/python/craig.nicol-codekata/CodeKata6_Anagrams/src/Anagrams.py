#!/usr/bin/env python
from __future__ import with_statement
from __future__ import print_function
from operator import attrgetter
import collections
import string

class anagram():
    """ anagram() - a solution for CodeKata6 - Anagrams
        # see http://codekata.pragprog.com/2007/01/kata_six_anagra.html
        # using wordlist from http://potm.tripod.com/BOGGLE/wordlist.txt
        # using longwordlist from http://addagram.mytestbench.com/WORD.LST
        
        #doctests
    >>> A = anagram("wordlist.txt")
    >>> A.filename
    'wordlist.txt'
    >>> d = A.readfile()
    >>> d.keys != None # Has keys
    True
    >>> A.printAnagrams(d) # doctest:+ELLIPSIS
    LENI NEIL
    ...
    BRAIN BRIAN
    >>> # Long word list
    >>> LWL = anagram("longwordlist.txt")
    >>> LWL.filename
    'longwordlist.txt'
    >>> d = LWL.readfile()
    >>> d.keys != None # Has keys
    True
    """
    _inputfile = ""
    
    def __init__(self, inputfile):
        self._inputfile = inputfile
        self.generateKey = self.listKey

    def listKey(self, w):
        chars = list(w)
        chars.sort()
        key = ''.join(chars)
        return key

    def stringKey(self, w): # doesn't work
        return (len(w), sum((string.letters.index(c) for c in list(w))))

    def multiHashKey(self, w):
        return (len(w), sum(hash(c) for c in list(w)))
        
    filename = property(attrgetter('_inputfile'), None)
    
    def readfile(self):
        anagramdict = collections.defaultdict(list)
        with open(self._inputfile) as f:
            for l in f.readlines():
                w = l.strip()
                key = self.generateKey(w)
                anagramdict[key].append(w)
        return anagramdict
    
    def printAnagrams(self, anagramdict):
        for k in anagramdict.keys():
            if len(anagramdict[k]) > 1:
                print (' '.join(anagramdict[k]))

def speedtest():
    import time
    start = time.clock()
    runcount = 10
    for i in xrange(runcount):
        print (".", end="")
        LWL = anagram("longwordlist.txt")
        d = LWL.readfile()
    end = time.clock()
    print ("Time taken for ", runcount, "runs :", end-start, "s; or", (end-start)/runcount, "s per run")
    
def _test():
    import doctest
    doctest.testmod()

if __name__ == "__main__":
    _test()
    speedtest()
    
    import cProfile
    cProfile.run('speedtest()')