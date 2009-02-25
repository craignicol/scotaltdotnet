$:.unshift(File.dirname(__FILE__) + '/../bin/Debug') 
require 'mscorlib'
require 'Horn.Core.DSL.Domain'

class MetaDataFactory
	def return_meta_data()
		meta = Horn::Core::DSL::Domain::BuildMetaData.new
		meta.Description = "A description of sorts"
	    meta
	end
end