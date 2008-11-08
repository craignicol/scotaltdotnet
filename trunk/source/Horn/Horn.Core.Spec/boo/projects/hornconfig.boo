install horn:
	description "This is a description of horn"
	get_from svn("https://svnserver/trunk")
	build_with nant("default.build")
	tasks one, two, three