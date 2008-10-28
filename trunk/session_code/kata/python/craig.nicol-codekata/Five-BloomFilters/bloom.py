#!/usr/bin/env python
from __future__ import with_statement

import collections
import sys
import string
import random

nonalphachars = string.punctuation + string.whitespace + string.digits

def byteSizedHashes(bighash, steps=2, hashbitdepth=32):
    factor = max(2**(hashbitdepth/steps),2)
    while bighash not in [0,-1]:
        smallhash, bighash = bighash % factor, bighash // factor
        yield smallhash

wordsfile = "/usr/share/dict/words"

bloomfilter = collections.defaultdict(bool)
with open(wordsfile) as f:
    for word in f.readlines():
        for bsh in byteSizedHashes(hash(word.strip())):
            bloomfilter[bsh] = True

inputfile = sys.argv[1] if len(sys.argv) > 1 else "/usr/share/doc/python2.5/examples/Demo/README"

def maybeWord(word):
    for bsh in byteSizedHashes(hash(word.lower())):
        if bloomfilter[bsh] == False:
           return False
    return True

def checkWord(w):
    print "maybeWord('"+w+"') = "+str(maybeWord(w))

checkWord("this")
checkWord("that")
checkWord("poaewuoia")
checkWord("with")

with open(inputfile) as f:
    transmap = ''.join([c if c in string.letters else ' ' for c in string.maketrans("","")])
    for line in f.readlines():
        for word in line.translate(transmap).split():
            if not maybeWord(word):
                print "Word: '"+word+"' not in dictionary."

def fiveCharWords(maxWords, wordLength=5):
    for i in xrange(maxWords):
        yield ''.join(random.sample(string.letters * wordLength, wordLength))

def countFalsePositives():
    wordset = {}
    with open(wordsfile) as f:
        for word in f.readlines():
            wordset[word] = True

    numtests = 1000000
    falsePositives = 0
    falseNegatives = 0

    falsePositives = len([w for w in fiveCharWords(numtests) if (maybeWord(w) and (w not in wordset))])
 
    print falsePositives, "false positives in", numtests, "trials = ", 100.0*falsePositives/numtests, "%"

countFalsePositives()
