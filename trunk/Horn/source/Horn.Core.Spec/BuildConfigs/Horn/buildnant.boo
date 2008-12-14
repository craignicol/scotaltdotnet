install horn:
	description "This is a description of horn"
	get_from svn("https://scotaltdotnet.googlecode.com/svn/trunk/")
	build_with nant, buildfile("Horn.build"), frameworkVersion35
	with:
		tasks build