install nhibernate:
	description "Nhibernate handles persisting plain .NET objects to and from an underlying relational database."
	get_from svn("https://nhibernate.svn.sourceforge.net/svnroot/nhibernate/trunk/nhibernate/")
	build_with nant, buildfile("default.build"), FrameworkVersion35	
		
	switches:
		parameters "with.examples=false"
		
	shared_library "lib/net/3.5"
	build_root_dir "build"		
	
dependencies:
	depend "castle.tools" >> "Castle.Core"
	depend "castle.tools" >> "Castle.DynamicProxy2"

package.homepage = "http://www.hibernate.org/343.html"
package.forum    = "http://groups.google.co.uk/group/nhusers?hl=en"
