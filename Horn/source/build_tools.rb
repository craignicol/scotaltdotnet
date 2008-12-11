require 'erb'
require 'fileutils'
require 'source/project_config'

def build relative_path
 # allows me to avoid duplicating the build location in the build file
 return File.join('build', relative_path)
end

def compile_dll(project_file)
  puts "#{BUILD_DIR}"
  sh "#{DOT_NET_PATH}MSBuild.exe #{project_file} -p:OutputPath=#{BUILD_DIR} -p:TreatWarningsAsErrors=true -p:SignAssembly=false -p:Configuration=#{PROJECT_CONFIGURATION};TargetFrameworkVersion=v3.5"
end

class Dir
  def self.remove(asset)  
    FileUtils.rm_r(asset)
  end
 
  def self.exists(asset)
    File.exists?(asset) && File.directory?(asset)
  end
  
  def self.make_chain(chain)
    FileUtils.makedirs chain
    puts "created directory #{chain}"    
  end
end

def replace_in_file(filename, s, r)
  lines = []
  File.open(filename, "r"){|f| lines = f.readlines }
  lines = lines.inject([]){|l, line| l << line.gsub(s, r)}
  File.open(filename, "w"){|f| f.write(lines) }
end

class XUnitRunner
  
  def initialize(assemblyToTest, assemblyName)
    @assemby = "#{assemblyToTest}.dll"
    @configruation = "#{assemblyToTest}.dll.config"
    @test_results = "#{TEST_RESULTS_DIR}/#{assemblyName}-results.xml"
    @output_file = "#{BUILD_DIR}/#{@assemby}-xunit-output.txt"
  end
  
  def run_tests
    sh "#{XUNIT_PATH} #{@assemby} #{@configuration} #{xml}"
  end
  
  def output
    "/out=#{@output_file}" if @output_file
  end
  
  def config
    "/config=#{@configruation}" if @configuration
  end
  
  def xml
    "/nunit #{@test_results}"
  end
end

class AsmInfoBuilder
	attr_reader :buildnumber

	def initialize(baseVersion, properties)
		@properties = properties;
		
		@buildnumber = baseVersion + (ENV["CCNetLabel"].nil? ? '0' : ENV["CCNetLabel"].to_s)
		@properties['Version'] = @properties['InformationalVersion'] = buildnumber;
	end

	def write(file)
		template = %q{
using System;
using System.Reflection;
using System.Runtime.InteropServices;

<% @properties.each {|k, v| %>
[assembly: Assembly<%=k%>Attribute("<%=v%>")]
<% } %>
		}.gsub(/^    /, '')
		  
	  erb = ERB.new(template, 0, "%<>")
	  
	  File.open(file, 'w') do |file|
		  file.puts erb.result(binding) 
	  end
	end
end
