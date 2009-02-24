$:.unshift(File.dirname(__FILE__) + '/../../lib') 
require 'mscorlib'

class SimpleType
	def return_string()
		"This should be returned"
	end
end