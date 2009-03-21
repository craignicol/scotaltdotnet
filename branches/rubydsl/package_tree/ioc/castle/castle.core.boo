install horn:
	description "Castle.core"
	get_from svn("http://svn.castleproject.org:8080/svn/castle/trunk/Core")
	build_with msbuild, buildfile("Core-vs2008.sln"), FrameworkVersion35