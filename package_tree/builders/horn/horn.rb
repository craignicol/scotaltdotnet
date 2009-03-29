require 'hornbuild'

install :horn do
  description "A .NET build and dependency manager"
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/Horn.Console/Horn.Console.csproj"
  get_from :svn, "http://scotaltdotnet.googlecode.com/svn/branches/rubydsl/"
  output "src/Horn.Console/bin/debug"
  shared_library "lib"
  
  dependency :log4net =>  "log4net"
  #dependency :castle =>   "castle.core"
end

project.homepage "http://code.google.com/p/scotaltdotnet/"
project.forum       "http://groups.google.co.uk/group/horn-development?hl=en"
project.contrib     false
