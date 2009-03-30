require 'hornbuild'

install :horn do
  description "A .NET build and dependency manager"
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/Horn.sln"
  get_from :svn, "http://scotaltdotnet.googlecode.com/svn/branches/rubydsl/"
  output "."
  shared_library "lib"
  
  dependency :log4net =>  "log4net"
  dependency :castle  =>  "castle.core"
  dependency :castle  =>  "Castle.DynamicProxy2"
  dependency :castle  =>  "castle.microKernel"
  dependency :castle  =>  "castle.windsor"
end

project.homepage "http://code.google.com/p/scotaltdotnet/"
project.forum       "http://groups.google.co.uk/group/horn-development?hl=en"
project.contrib     false
