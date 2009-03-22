require 'hornbuild'

install :horn do
  build_with :msbuild, :frameworkVersion35, :buildfile => "src/horn.sln"
end
