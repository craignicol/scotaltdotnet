install castle.tools:
	description "Castle is an open source project for .net that aspires to simplify the development of enterprise and web applications."
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/")
	
	prebuild:
		cmd "xcopy /s /y \"../Patch\" ."
		
	/*export:
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/Core/Castle.Core", to("Core"))
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/Tools", to("Tools"))  		
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/common.xml")  		
		svn("http://svn.castleproject.org:8080/svn/castle/trunk/common-project.xml")*/
	
	build_with nant, buildfile("Tools/Tools.build"), FrameworkVersion35
		
	switches:
		parameters "sign=true","common.testrunner.enabled=false"
		
	shared_library "SharedLibs/net/2.0"
	output "build/net-3.5/debug"		
	
package.homepage = "http://www.castleproject.org/"
package.forum    = "http://groups.google.co.uk/group/castle-project-users?hl=en"