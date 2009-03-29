install castle:
	description "Castle is an open source project for .net that aspires to simplify the development of enterprise and web applications."
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/")
	build_with nant, buildfile("default.build"), FrameworkVersion35
	shared_library "SharedLibs/net/2.0"
	output "build/net-3.5/debug"
	with:
		tasks build release quick rebuild
		
	switches:
		parameters "mandatory=false"
	
dependencies:
	depend @log4net >> "log4net"