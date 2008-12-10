..\..\lib\nant\nant -t:net-3.5 -buildfile:Horn.build -D:sign=false -D:testrunner=NUnit -D:common.testrunner.enabled=true  -D:environment=uat -D:common.testrunner.failonerror=true -D:build.msbuild=true

PAUSE
