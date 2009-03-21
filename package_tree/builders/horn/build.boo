install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35
	
metadata:
    data "contrib=false", "createdate=24/01/2009"
    data "France=yuky"

dependencies:
	depend @log4net >> @lib	
