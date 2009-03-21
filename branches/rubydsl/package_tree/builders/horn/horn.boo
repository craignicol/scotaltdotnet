﻿install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35

dependencies:
	depend @log4net >> "log4net"
	depend @castle  >> "castle.core"
	depend @castle  >> "castle.dynamicproxy"
	#depend @castle  >> "castle.microkernel"
	#depend @castle  >> "castle.windsor"
