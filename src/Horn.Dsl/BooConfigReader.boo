namespace Horn.Dsl
import System
import System.Collections.Generic
import Boo.Lang
import Boo.Lang.Compiler
import Boo.Lang.Compiler.Ast
import Horn.Domain
import System.Runtime.CompilerServices	    

abstract class BooConfigReader: 
	callable Action()      
	
	[Meta]
	static def install(expression as ReferenceExpression, action as Expression):  	
		name = StringLiteralExpression(expression.Name) 
		
		return [|
			self.GetInstallerMeta($name, $action)
		|]
		
	def description(text as string):
		desc = text
		
	[Meta]
	static def export_from(expression as MethodInvocationExpression):
		return expression
	
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

	def AddDependencies(dependencies as (string)):
		for i in range(dependencies.Length):
			dependency = BuildEngines.Dependency(dependencies[i].Split(Char.Parse('|'))[0], dependencies[i].Split(Char.Parse('|'))[1])
			BuildEngine.Dependencies.Add(dependency)

	def GetInstallerMeta(name as string, action as Action):
		installName = name
		action()
		
	def msbuild(buildFile as string, frameworkVersion):
		version = System.Enum.Parse(typeof(Horn.Domain.Framework.FrameworkVersion), frameworkVersion)
		BuildEngine = BuildEngines.BuildEngine(Horn.Domain.MSBuildBuildTool(), buildFile, version)
		
	def svn(url as string):		
			

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
	
	public MetaData as Package:
		get:
			return package
	    
	abstract def Prepare():
		pass	  		

macro output: 
	assert output.Arguments.Count == 1
	value = (output.Arguments[0] as Ast.StringLiteralExpression).Value
	return [|
			block:
				 BuildEngine.OutputDirectory = $value
	|].Block

macro shared_library: 
	assert shared_library.Arguments.Count == 1
	value = (shared_library.Arguments[0] as Ast.StringLiteralExpression).Value
	return [|
			block:
				 BuildEngine.SharedLibrary = $value
	|].Block
	





























class MyDslAsAnonymousBaseClassStep(AbstractTransformerCompilerStep):
	# This is the method that the compiler pipeline will execute
	def Run():
		# The visitor pattern make it very easy to pick and choose
		# what we want to process
		Visit(CompileUnit)
	# Here we are choosing to process all the modules
	override def OnModule(node as Module):
		
		# We use quasi quotation to generate a class definition
		# with all the code that was in the module inside it.
		# We use the same name as the module and inherit from MyDsl
		baseClass = [|
			class $(node.Name) (BooConfigReader):
				override def Build():
					$(node.Globals)
		|]
	
		# Then we clear the module’s globals and add the newly
		# created class to the node’s members.
		node.Globals = Block()
		node.Members.Add(baseClass)
