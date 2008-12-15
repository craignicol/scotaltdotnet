require 'rake'
require 'build_tools'
  
require 'ftools'

task :default => :build 

task :build  => [:clean, :init, :copy_referenced_assemblies, :build_horn_core, :build_horn_console] do
  
end 

task :build_horn_core => [:generate_assembly_info] do
  compile_dll "Horn.Core/Horn.Core.csproj"
end

task :build_horn_console do
  compile_dll "Horn.Console/Horn.Console.csproj"
  if ENV["runtests"] == "true"
    Rake::Task["build_horn_spec"].execute 
  end
end

task :build_horn_spec_framework do
 compile_dll "Horn.Spec.Framework/Horn.Spec.Framework.csproj"
end

task :build_horn_spec => [:build_horn_spec_framework] do
  compile_dll "Horn.Core.Spec/Horn.Core.Spec.csproj"
  xunitRunner = XUnitRunner.new("../src/Horn.Core.Spec/bin/Debug/Horn.Core.Spec", "Horn.Core.Spec")
  xunitRunner.run_tests
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
  files = Dir.glob(File.join("#{BUILD_LIB_DIR}/**", "*.dll")) + Dir.glob(File.join("#{BUILD_LIB_DIR}/**", "*.pdb")) + Dir.glob(File.join("#{BUILD_LIB_DIR}/**", "*.config")) + Dir.glob(File.join("#{BUILD_LIB_DIR}/**", "*.xml")) + Dir.glob(File.join("#{BUILD_LIB_DIR}/**", "*.exe"))
  files.each do |item|
    cp_r item, "#{BUILD_DIR}"
  end
end
