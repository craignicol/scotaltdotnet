namespace Horn.Boo.Dsl
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
	    
	abstract def Prepare():
	  pass

	[Meta]
	static def install(expression as ReferenceExpression, action as Expression):  	
		name = StringLiteralExpression(expression.Name) 
		
		return MethodInvocationExpression(ReferenceExpression("GetInstallerMeta"), name, action)

	def GetInstallerMeta(name as string, action as Action):
	  installName = name
	  action()

	def description(text as string):
		desc = text
