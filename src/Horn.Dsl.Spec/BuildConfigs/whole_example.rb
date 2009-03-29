require 'hornbuild'

install :horn do
  description "A .NET build and dependency manager"
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/horn.sln"
  get_from :svn, "http://scotaltdotnet.googlecode.com/svn/trunk/"
  output "Output"
  shared_library "."
  
  dependency :boo =>       "boo.lang"
  dependency :boo =>       "boo.lang"
  dependency :boo =>       "boo.langCompiler"
  dependency :boo =>       "boo.lang.parser"
  dependency :castle =>    "castle.core"
  dependency :castle =>    "castle.microKernel"
  dependency :castle =>    "castle.windsor"
  dependency :log4net =>   "log4net"
  dependency :rhino =>      "rhino.dsl"
  dependency :rhino =>      "rhino.mocks"  
end

project.homepage "http://code.google.com/p/scotaltdotnet/"
project.forum       "http://groups.google.co.uk/group/horn-development?hl=en"
project.contrib     false
