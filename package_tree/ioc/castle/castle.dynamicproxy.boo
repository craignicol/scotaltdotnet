install horn:
	description "Castle Dynamic proxy"
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/Tools/DynamicProxy")
	build_with msbuild, buildfile("Castle.Windsor-vs2008"), FrameworkVersion35

dependencies:
	depend @castle  >> "castle.core"