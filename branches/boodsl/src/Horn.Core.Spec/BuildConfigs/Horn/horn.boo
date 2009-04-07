install horn:
	description "A .NET build and dependency manager"
	export_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")  
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35	
output "Output"
shared_library "."	

dependencies:
	depend @log4net >> @lib  
	
#metadata:
#    data "contrib=false", "createdate=24/01/2009"
#    data "France=yuky"

