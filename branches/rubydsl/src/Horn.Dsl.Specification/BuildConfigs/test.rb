

class MetaDataFactory
	def return_meta_data()
		meta = Horn::Core::DSL::Domain::BuildMetaData.new
		meta.Description = "A description of sorts"
		dependency = Horn::Core::DSL::Domain::Dependency.new
		dependency.Id = 1
		dependency.Value = "This is a dependency"
		meta.Dependencies.Add(dependency)
    meta
	end
end