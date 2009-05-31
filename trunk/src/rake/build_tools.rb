require 'erb'
require 'ftools'
require 'fileutils'
require 'project_config'

module Horn
  module BuildTools
    class NantRunner
        class << self
          def nant(build_file = 'ncontinuity2.build', *tasks)
            tasks_to_run = tasks.map {|t| t.to_s}.join(', ') 
            sh "#{@nant} -f:#{build_file} -t:net-3.5 -D:sign=false -D:testrunner=NUnit -D:common.testrunner.enabled=false  -D:environment=uat -D:common.testrunner.failonerror=true -D:build.msbuild=true  #{tasks_to_run}"
          end
        end
    end
  
    class MSbuild
      class << self
        def compile_dll(project_file)
          puts "#{BUILD_DIR}"
          sh "#{DOT_NET_PATH}MSBuild.exe #{project_file} -p:OutputPath=#{BUILD_DIR} -p:TreatWarningsAsErrors=true -p:SignAssembly=false -p:Configuration=#{PROJECT_CONFIGURATION};TargetFrameworkVersion=v3.5"
        end
      end
    end

    def replace_in_file(filename, s, r)
      lines = []
      File.open(filename, "r"){|f| lines = f.readlines }
      lines = lines.inject([]){|l, line| l << line.gsub(s, r)}
      File.open(filename, "w"){|f| f.write(lines) }
    end
  end
end

class Dir
  class << self
    def delete_dir(asset)  
      FileUtils.rm_r(asset)
    end
    
    def copy_filetypes(source, destination, mask)
      fileMask = "*." << mask

      if  not dir_exists(destination)
        Dir.mkdir destination
      end
      
      Dir.glob(File.join(source,fileMask)).each do|item|
        cp_r item, destination
      end
    end
    
    def remove(asset)  
      FileUtils.rm_r(asset)
    end
   
    def exists(asset)
      File.exists?(asset) && File.directory?(asset)
    end    
    
    def copy_assemblies(source, destination, mask)
      files = Dir.glob(File.join("lib/**", "#{mask}.dll")) + Dir.glob(File.join("#{source}/**", "#{mask}.pdb")) + Dir.glob(File.join("#{source}/**", "#{mask}.config")) + Dir.glob(File.join("#{source}/**", "#{mask}.xml")) + Dir.glob(File.join("#{source}/**", "#{mask}.exe"))
      files.each do |item|
        cp_r item, "#{destination}"
      end    
    end
   
    def dir_exists(asset)
      File.exists?(asset) && File.directory?(asset)
    end
    
    def make_chain(chain)
      FileUtils.makedirs chain
      puts "created directory #{chain}"    
    end
  end
end