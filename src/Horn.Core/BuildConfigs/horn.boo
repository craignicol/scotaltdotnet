install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35
	shared_library "."
	
dependencies:
	depend @log4net >> @lib	
