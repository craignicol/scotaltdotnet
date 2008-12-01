install horn:
	description "This is a description of horn"
	get_from svn("https://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("horn.sln"), frameworkVersion35