module MetaBuilder
  module Dsl
    module Main
      
      def install(name, &block)        
        yield self if block_given?
      end

      def get_from(name, url)
        puts name
        puts url
      end

      def description(desc)
         wrapper.metadata.Description = desc
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

def wrapper
  MetaBuilder::Dsl::Main::MetaDataAccessor.instance
end

class ClrAccessor
  def get_build_metadata
    wrapper.metadata
  end
end

include MetaBuilder::Dsl::Main