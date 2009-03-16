require 'lib/dsl'
class MetaData    
  include Horn::Dsl::Main
  
  attr_accessor :desc
  
  def setDescription(desc)
    @desc = desc
  end
end