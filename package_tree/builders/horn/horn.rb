require 'hornbuild'

install :horn do
  description "A .NET build and dependency manager"
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/horn.sln"
  get_from :svn, "http://scotaltdotnet.googlecode.com/svn/trunk/"
  
  dependency :log4net =>  "log4net"
end

project.homepage "http://code.google.com/p/scotaltdotnet/"
project.forum       "http://groups.google.co.uk/group/horn-development?hl=en"
project.contrib     false
