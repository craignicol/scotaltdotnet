class BuildMetaDataParser
  attr_accessor :name
  def initialize(name)
    @name = name
    puts name
    yield(self)
  end
end



BuildMetaDataParser.new :horn do 
  #puts s.name
end