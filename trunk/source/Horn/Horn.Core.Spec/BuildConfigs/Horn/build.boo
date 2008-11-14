install horn:
	description "This is a description of horn"
	get_from svn("https://svnserver/trunk")
	build_with rake("rakefile.rb"), with:
		tasks build, test, deploy