install nhibernate:
	description "Nhibernate handles persisting plain .NET objects to and from an underlying relational database."
	get_from svn("https://nhibernate.svn.sourceforge.net/svnroot/nhibernate/trunk/nhibernate/")
	build_with nant, buildfile("default.build"), FrameworkVersion35	
		
	switches:
		parameters "with.examples=false"
		
	shared_library "lib/net/3.5"
	output "build/NHibernate-2.1.0.Alpha2-debug/bin/net-3.5"		
	
dependencies:
	depend @log4net >> "log4net"
	depend @castle  >>  "Castle.Core"
	depend @castle  >>  "Castle.DynamicProxy2"