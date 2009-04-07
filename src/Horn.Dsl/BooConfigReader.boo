namespace Horn.Dsl
import System
import Boo.Lang
import Boo.Lang.Compiler
import Boo.Lang.Compiler.Ast
import Horn.Domain

abstract class BooConfigReader: 
	callable Action()   
    
	[property(BuildEngine)]
	public buildEngine as BuildEngines.BuildEngine    
    
	[property(Description)]
	public desc as string
	
	[property(InstallName)]
	public installName as string
	
	[property(Output)]
	public outputDirectory as string		
	
	[property(SharedLibrary)]
	public library as string	
		
	[property(SourceControl)]
	public sourceControl as Horn.Domain.SCM.SourceControl
	    
	abstract def Prepare():
	  pass
	
	[Meta]
	static def build_with(builder as ReferenceExpression, build as MethodInvocationExpression, frameWorkVersion as ReferenceExpression):
		buildFile = build.Arguments[0]
		version = StringLiteralExpression(frameWorkVersion.Name)	
			
		return [|
			$builder($buildFile, $version)
		|]
									
	[Meta]
	static def dependencies(addDependencyMethod as MethodInvocationExpression):
		return addDependencyMethod
					       		
	[Meta]
	static def export_from(expression as MethodInvocationExpression):
		return expression

	[Meta]
	static def install(expression as ReferenceExpression, action as Expression):  	
		name = StringLiteralExpression(expression.Name) 
		
		return [|
			self.GetInstallerMeta($name, $action)
		|]

	def AddDependencies(dependencies as (string)):
		for i in range(dependencies.Length):
			dependency = Horn.Domain.BuildEngines.Dependency(dependencies[i].Split(Char.Parse('|'))[0], dependencies[i].Split(Char.Parse('|'))[1])
			BuildEngine.Dependencies.Add(dependency)

	def description(text as string):
		desc = text

	def GetInstallerMeta(name as string, action as Action):
		installName = name
		action()
		
	def msbuild(buildFile as string, frameworkVersion):
		version = System.Enum.Parse(typeof(Horn.Domain.Framework.FrameworkVersion), frameworkVersion)
		BuildEngine = BuildEngines.BuildEngine(Horn.Domain.MSBuildBuildTool(), buildFile, version)
		
	def svn(url as string):		
		sourceControl = SCM.SVNSourceControl(url)	  
	  		

macro output: 
	assert output.Arguments.Count == 1
	value = (output.Arguments[0] as Ast.StringLiteralExpression).Value
	code = [|
			block:
				 BuildEngine.OutputDirectory = $value
	|]
	return code.Blockmacro shared_library: 
	assert shared_library.Arguments.Count == 1
	value = (shared_library.Arguments[0] as Ast.StringLiteralExpression).Value
	code = [|
			block:
				 BuildEngine.SharedLibrary = $value
	|]
	return code.Block