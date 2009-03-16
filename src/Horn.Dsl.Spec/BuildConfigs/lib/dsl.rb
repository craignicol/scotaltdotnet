module MetaBuilder
  module Dsl
    module Main
      attr_accessor :metadata
      def install(name, &block)
        @metadata = Horn::Core::Dsl::BuildMetaData.new
        yield self if block_given?
        #puts @metadata.description
        puts "it works"
      end

      def get_from(name, url)
        puts name
        puts url
      end

      def description(desc)
         @metadata.Description = desc
      end
       
      def get_metadata
          @metadata
      end

    end
  end
end

include MetaBuilder::Dsl::Main