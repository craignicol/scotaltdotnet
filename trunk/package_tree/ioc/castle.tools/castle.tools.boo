install castle.tools:
	description "Dynamic Proxy Generator for the CLR."
	
	prebuild:
		cmd "xcopy /s /y \"../Patch\" ."
		
	include:
		repository(castle, part("Tools"), to("Tools"))
		repository(castle, part("Core"), to("Core"))
		repository(castle, part("common.xml"), to("common.xml"))
		repository(castle, part("common-project.xml"), to("common-project.xml"))
		repository(castle, part("CastleKey.snk"), to("CastleKey.snk"))
		/*svn("http://svn.castleproject.org:8080/svn/castle/trunk/Tools", to("Tools")) 
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/Core", to("Core")) 		
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/common.xml", to("common.xml"))  		
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/common-project.xml", to("common-project.xml"))
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/CastleKey.snk", to("CastleKey.snk"))*/

	build_with nant, buildfile("Tools/Tools.build"), FrameworkVersion35
		
	switches:
		parameters "sign=true","common.testrunner.enabled=false"
		
	shared_library "SharedLibs/net/2.0"
	output "build/net-3.5/debug"		
	
package.homepage = "http://www.castleproject.org/"
package.forum    = "http://groups.google.co.uk/group/castle-project-users?hl=en"  