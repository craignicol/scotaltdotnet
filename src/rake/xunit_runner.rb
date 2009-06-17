module Horn
  module TestRunners
    class XUnitRunner
      
      def initialize(assemblyToTest)
        assemblyName = assemblyToTest.split('/').last
        @assemby = "#{assemblyToTest}.dll"
        @configruation = "\"#{assemblyToTest}.dll.config\""
        @test_results = "\"#{TEST_RESULTS_DIR}#{assemblyName}-results.xml\""
        @output_file = "\"#{BUILD_DIR}#{@assemby}-xunit-output.txt\""
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
  end
end