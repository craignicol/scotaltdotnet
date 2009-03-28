install log4net:
	description "A .NET build and dependency manager"
	get_from svn("http://svn.apache.org/repos/asf/logging/log4net/trunk/")
	build_with nant, buildfile("src/horn.build"), FrameworkVersion35
	with:
		tasks build
	switches:
		#parameters "sign=false", "testrunner=NUnit", "common.testrunner.enabled=true", "common.testrunner.failonerror=true", "build.msbuild=true"
	
