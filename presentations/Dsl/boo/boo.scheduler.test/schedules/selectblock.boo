task "90 day password reminder":

	select "SELECT fullname, email FROM Users WHERE DATEDIFF(d, lastlogindate, getdate()) > 82"

	subject "ACME Password Expiry"