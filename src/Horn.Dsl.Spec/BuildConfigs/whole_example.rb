require 'hornbuild'

install :horn do
  description "A .NET build and dependency manager"
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/horn.sln"
  get_from :svn, "http://scotaltdotnet.googlecode.com/svn/trunk/"
  
  dependency :Boo =>      "Boo.Lang"
  dependency :Boo =>      "Boo.Lang"
  dependency :Boo =>      "boo.langCompiler"
  dependency :Boo =>      "Boo.Lang.Parser"
  dependency :Castle =>   "Castle.Core"
  dependency :Castle =>   "Castle.MicroKernel"
  dependency :Castle =>   "Castle.Windsor"
  dependency :log4net =>  "log4net"
  dependency :Rhino =>    "Rhino.Dsl"
  dependency :Rhino =>    "Rhino.Mocks"  
end

project.homepage "http://code.google.com/p/scotaltdotnet/"
project.forum       "http://groups.google.co.uk/group/horn-development?hl=en"
project.contrib     false
