task "90 day password reminder":
	
	subject "ACME Password Expiry"
  
	query "SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"
  
	clients:
		include @fakeclient	 
  
	parameters:  
		list @DisplayName, @Url
	
	every 7.Days()
	starting now
	enabled