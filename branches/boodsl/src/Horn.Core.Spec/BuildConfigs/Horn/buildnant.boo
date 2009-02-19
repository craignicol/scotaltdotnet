install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with nant, buildfile("src/horn.build"), frameworkVersion35
	with:
		tasks build
	switches:
		parameters "sign=false", "testrunner=NUnit", "common.testrunner.enabled=true", "common.testrunner.failonerror=true", "build.msbuild=true"

metadata:
    data "contrib=false", "createdate=24/01/2009"
    data "France=yuky"

dependencies:
	depend @log4net >> @lib
