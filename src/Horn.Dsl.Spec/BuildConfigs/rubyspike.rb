class MetaDataFactory
	def return_meta_data()
		meta = Horn::Core::dsl::BuildMetaData.new
		meta.Description = "A description of sorts"
    meta
	end
end