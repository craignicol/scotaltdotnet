install log4net:
	description "log4net is a tool to help the programmer output log statements to a variety of output targets"
	get_from svn("http://svn.apache.org/repos/asf/logging/log4net/trunk/")
	output "bin/net/2.0/debug"
	build_with nant, buildfile("log4net.build"), FrameworkVersion35
	
	switches:
		parameters "mandatory=false"
	
