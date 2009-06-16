install automapper:
	description "A convention-based object-object mapper in .NET."
	get_from svn("http://automapperhome.googlecode.com/svn/trunk/")
	
	build_with nant, buildfile("AutoMapper.build"), FrameworkVersion35
	
	with:
		tasks full			
		
	output "build"
	
package.homepage = "http://www.codeplex.com/AutoMapper/"
package.forum    = "http://groups.google.com/group/automapper-users"

	
