require 'lib/dsl'
class MetaData    
  include MetaBuilder::Dsl::Main
  
  attr_accessor :desc
  
  def setDescription(desc)
    @desc = desc
  end
end