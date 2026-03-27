<!--- Class Diagram for GMMR Systems -->

# GMMR Systems
````mermaid
---
title: GMMR Systems
---
classDiagram
	class Volunteer{
	-String ID
	-String Name
	-String PhoneNo
	-String email
	}
	class Vehicle{
	-String ID
	-String Plate
	-String Make
	-String Model
	-Enum Tranmission
	-String Year
	+register()
	}
	class Owner{
	-String ID
	-String Name
	-String PhoneNo
	}
	class Booking{
	-String ID
	-String Plate
	-String OwnerID
	-Decimal Cost
	-String Time
	-String Description
	-Date Date
	}
	class Lecturer{
	-String ID
	-String Name
	-String PhoneNo
	-String Email
	}
	class Trainee{
	-String ID
	-String Name
	-String PhoneNo
	-String Email
	}
	class User{
	-String username
	-String password
	-String email
	}
	
