module MetaBuilder
  module Dsl
    module Main
      
      def install(name, &block)        
        yield self if block_given?
      end

      def get_from(name, url)
        case name
          when :svn then meta.metadata.SourceControl = Horn::Core::SCM::SVNSourceControl.new(url)
        end
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