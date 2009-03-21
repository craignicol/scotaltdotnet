install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/Tools/Castle.DynamicProxy2/Castle.DynamicProxy")
	build_with msbuild, buildfile("Castle.DynamicProxy-vs2008.csproj"), FrameworkVersion35

dependencies:
	depend @log4net >> "log4net"
	depend @castle  >> "castle.core"