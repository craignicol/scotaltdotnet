module Horn
  module Dsl
    module Main
      def install(name, &block)
        puts "4"
        yield self if block_given?
      end      
      
      def include(*args, &block)
        puts "include"
      end
    end
  end
 end