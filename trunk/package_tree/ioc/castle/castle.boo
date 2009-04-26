install castle:
	description "Castle is an open source project for .net that aspires to simplify the development of enterprise and web applications."
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/")
	
	build_with nant, buildfile("default.build"), FrameworkVersion35
	
	with:
		tasks quick, rebuild
		
	switches:
		parameters "sign=true","build.warnaserrors=false"
		
	shared_library "SharedLibs/net/2.0"
	output "build/net-3.5/debug"		
	
dependencies:    
	depend @boo		>> "Boo.Lang.Extensions"
	depend @boo		>> "Boo.Lang.Interpreter"
	depend @boo		>> "Boo.Lang.Parser"
	depend @boo		>> "Boo.Lang.Useful"
	depend @boo		>> "Boo.NAnt.Tasks"  
	depend @boo		>> "Boo.Lang.CodeDom"
	depend @boo		>> "Boo.Lang.Compiler"	
	depend @boo		>> "booc"
	depend @boo		>> "Boo.Lang"
	#depend @log4net >> "log4net"
	
package.homepage = "http://www.castleproject.org/"
package.forum    = "http://groups.google.co.uk/group/castle-project-users?hl=en"