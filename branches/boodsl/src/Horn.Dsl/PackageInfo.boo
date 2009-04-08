namespace Horn.Dsl
import System
import System.Collections.Generic
import Boo.Lang
import Boo.Lang.Compiler
import Boo.Lang.Compiler.Ast
import System.Runtime.CompilerServices

[CompilerGlobalScopeAttribute]
class Global:
	public static package = Package()

class Package(IQuackFu): 
  
	[property(PackageInfo)]
	public packageInfo as Dictionary[of string, object]
  
	def QuackGet(name as string, parameters as (object)) as object:
		pass
	
	def QuackInvoke(name as string, args as (object)) as object:
		pass
	
	def QuackSet(name as string, parameters as (object), value) as object:  
		packageInfo.Add(name, value)

	def constructor():
		packageInfo = Dictionary[of string, object]()	    