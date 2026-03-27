<!--- Class Diagram for GMMR Systems -->

<!-- TODO: Finish and check about using defined object type for classes like trainingclass? -->

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
	class TrainingClass{
	-String code
	-Volunteer ID
	-String name
	-Enum type
	-Date date 
	}
	
	