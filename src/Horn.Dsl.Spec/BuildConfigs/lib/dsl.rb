module MetaBuilder
  module Dsl
    module Main
      
      BuildEngines = Horn::Core::BuildEngines
      Framework = Horn::Core::Utils::Framework
      
      def install(name, &block)        
        yield self if block_given?
      end
      
      def build_with(buildEngine, version, options = {})
        raise "No such buildengine is implemented" unless respond_to? buildEngine
        buildTool = send(buildEngine)
        
        frameWorkVersion = version == :FrameworkVersion2 ? Framework::FrameworkVersion.FrameworkVersion2 : Framework::FrameworkVersion.FrameworkVersion35
        meta.metadata.BuildEngine = BuildEngines::BuildEngine.new(buildTool, options[:buildfile], frameWorkVersion)
      end
      
      def msbuild()
        Horn::Core::MSBuildBuildTool.new
      end

      def get_from(scm, url)
        raise "No such source control manager is implemented" unless respond_to? scm
        send(scm, url)
      end
      
      def svn(url)
        meta.metadata.SourceControl = SCM::SVNSourceControl.new(url)
      end

      def description(desc)
         meta.metadata.Description = desc
      end       

      class MetaDataAccessor
        attr_accessor :metadata
        
        private
        def initialize
          @metadata = Horn::Core::Dsl::BuildMetaData.new
        end
        
        public
        def self.instance
          @@instance ||= new
        end
        
        def self.get_metadata
            @metadata
        end        
      end
    end
  end
end

def meta
  MetaBuilder::Dsl::Main::MetaDataAccessor.instance
end

class ClrAccessor
  def get_build_metadata
    meta.metadata
  end
end

include MetaBuilder::Dsl::Main