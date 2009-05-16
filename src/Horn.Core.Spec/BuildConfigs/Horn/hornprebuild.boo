﻿install horn:
	description "A .NET build and dependency manager"
	get_from svn("http://scotaltdotnet.googlecode.com/svn/trunk/")
	
	prebuild:
	  cmd "dir"
	  cmd "@echo \"hello\""
	
	build_with msbuild, buildfile("src/horn.sln"), FrameworkVersion35
	output "Output"
	shared_library "."
	
                
dependencies:
	depend @log4net >> "lib"
	depend @castle  >> "castle.core"
	depend @castle  >> "Castle.DynamicProxy2"
	depend @castle  >> "castle.microKernel"
	depend @castle  >> "castle.windsor"
	
package.homepage = "http://code.google.com/p/scotaltdotnet/"
package.forum    = "http://groups.google.co.uk/group/horn-development?hl=en"
package.contrib  = false