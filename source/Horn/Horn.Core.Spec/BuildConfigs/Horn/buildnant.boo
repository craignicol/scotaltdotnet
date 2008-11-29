install horn:
	description "This is a description of horn"
	get_from svn("https://svnserver/trunk")
	build_with nant("Horn.build"), with:
		tasks build