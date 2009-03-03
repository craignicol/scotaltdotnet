require 'horn'

describe BuildMetaData do
  it "should be able to store a descrption" do
    BuildMetaData.new.install :test do
      puts "5"
    end
  end
end

