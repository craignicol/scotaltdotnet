install horn:
	description "This is a description of horn"
	get_from svn("https://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with nant, buildfile("src/horn.build"), frameworkVersion35
	with:
		tasks build
	switches:
		parameters "sign=false", "testrunner=NUnit", "common.testrunner.enabled=true", "common.testrunner.failonerror=true", "build.msbuild=true"