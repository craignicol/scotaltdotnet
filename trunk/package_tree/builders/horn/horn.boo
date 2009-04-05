install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35	

dependencies:
	depend @log4net >>  "l0g4net"	
	depend @castle  >>  "castle.core"
	depend @castle  >>  "Castle.DynamicProxy2"
	depend @castle  >>  "castle.microKernel"
	depend @castle  >>  "castle.windsor"
