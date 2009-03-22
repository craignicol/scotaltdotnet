require 'hornbuild'

install :horn do
  #needed to create the BuildEngine
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/horn.sln"
  
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
