namespace Horn.Dsl
import System
import Boo.Lang
import Boo.Lang.Compiler
import Boo.Lang.Compiler.Ast

abstract class BooConfigReader: 
	callable Action()
    
	[property(Description)]
	public desc as string
	
	[property(InstallName)]
	public installName as string
	
	[property(SharedLibrary)]
	public library as string	
	
	[property(Output)]
	public outputDirectory as string		
	    
	abstract def Prepare():
	  pass

	[Meta]
	static def install(expression as ReferenceExpression, action as Expression):  	
		name = StringLiteralExpression(expression.Name) 
		
		return [|
			self.GetInstallerMeta($name, $action)
		|]

	def GetInstallerMeta(name as string, action as Action):
		installName = name
		action()

	def description(text as string):
		desc = text		

macro output: 
	assert output.Arguments.Count == 1
	value = (output.Arguments[0] as Ast.StringLiteralExpression).Value
	code = [|
			block:
				 outputDirectory = $value
	|]
	return code.Blockmacro shared_library: 
	assert shared_library.Arguments.Count == 1
	value = (shared_library.Arguments[0] as Ast.StringLiteralExpression).Value
	code = [|
			block:
				 library = $value
	|]
	return code.Block