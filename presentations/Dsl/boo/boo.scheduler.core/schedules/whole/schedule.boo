task "90 day password reminder":
	clients:
		include @fakeclient
	  
	select "SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"

	subject "ACME Password Expiry"
  
	parameters:  
		list @DisplayName, @Url
	
	every 7.Days()
	starting now
	enabled