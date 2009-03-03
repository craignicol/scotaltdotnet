class BuildMetaData
  attr_accessor :name
  
  def install(name, &block)
    @name = name
    puts "4"
    yield self if block_given?
    puts @name
  end
end

=begin
BuildMetaData.new :horn do 
  #puts s.name
end
=end