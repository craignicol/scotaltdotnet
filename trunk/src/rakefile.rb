$:.unshift File.join(File.dirname(__FILE__),'.','rake')
ROOT_DIR = File.expand_path(File.dirname(__FILE__) )

require 'rake'
require 'build_tools'
require 'asm_info_builder'
require 'xunit_runner'
  
require 'ftools'

BuildTools = Horn::BuildTools
Runners = Horn::TestRunners

task :default => :build 

task :build  => [:clean, :init, :copy_referenced_assemblies, :build_horn_core, :build_horn_console] do
  
  if ENV["runtests"] == "true"
    Rake::Task["build_horn_spec"].execute         
    Rake::Task["build_horn_integration"].execute 
  end  
  
end 

task :build_horn_core => [:generate_assembly_info] do
  BuildTools::MSbuild::compile_dll "Horn.Core/Horn.Core.csproj"
end

task :build_horn_console do  
  BuildTools::MSbuild::compile_dll "Horn.Console/Horn.Console.csproj"
end

task :_framework do
 BuildTools::MSbuild::compile_dll "Horn.Spec.Framework/Horn.Spec.Framework.csproj"
end

task :copy_package_tree do
  Dir.make_chain "#{BUILD_DIR}/BuildConfigs/Horn"
  Dir.copy_filetypes("#{ROOT_DIR}/Horn.Core.Spec/BuildConfigs/Horn", "#{BUILD_DIR}/BuildConfigs/Horn", "boo")
end

task :build_horn_spec => [:copy_package_tree, :build_horn_spec_framework] do
  
  BuildTools::MSbuild::compile_dll "Horn.Core.Spec/Horn.Core.Spec.csproj"
  
  cp_r Dir.glob(File.join("#{ROOT_DIR}/Horn.Core.Spec/BuildConfigs/**", "*.boo")), "#{BUILD_DIR}"
  
  Runners::XUnitRunner.new("#{BUILD_DIR}/Horn.Core.Spec").run_tests    
 end
 
task :build_horn_integration do
  BuildTools::MSbuild::compile_dll "Horn.Core.Integration/Horn.Core.Integration.csproj"  
  cp_r Dir.glob(File.join("#{ROOT_DIR}/Horn.Core.Integration/BuildConfigs/**", "*.boo")), "#{BUILD_DIR}"  
  Runners::XUnitRunner.new("#{BUILD_DIR}/Horn.Core.Integration").run_tests 
end

task :generate_assembly_info do
  myfile = File.new("#{COMMON_ASSEMBLY_INFO}", "w").close
  builder = AsmInfoBuilder.new(BUILD_NUMBER, {'Product' => PRODUCT, 'Copyright' => COPYRIGHT, 'Company' => COMPANY})
  puts "The build number is #{builder.buildnumber}"
  builder.write COMMON_ASSEMBLY_INFO 
end

task :clean do
  if Dir.exists(BUILD_BASE_DIR)
    Dir.remove BUILD_BASE_DIR
    puts "Deleted build directory #{BUILD_BASE_DIR}"
  else
    puts "build directory #{BUILD_BASE_DIR} does not exist"
  end
end

task :init do
  Dir.make_chain BUILD_DIR
  
  Dir.make_chain TEST_RESULTS_DIR
end

task :copy_referenced_assemblies do
  Dir.copy_assemblies(BUILD_LIB_DIR, BUILD_DIR, "*")
end
