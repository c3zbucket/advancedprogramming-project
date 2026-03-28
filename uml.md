<!--- Class Diagram for GMMR Systems -->

<!-- TODO: Finish and check about using defined object type for classes like trainingclass? -->

````mermaid
---
title: GMMR Systems Layout
---
classDiagram
	class Volunteer{
	-ID : String
	-Name : String
	-PhoneNo : String
	-email : String
	}
	class Transmission {
	<<Enumeration>>
	MANUAL
	AUTOMATIC
	SEMI-AUTO
	}
	class Engine {
	<<Enumeration>>
	PETROL
	DIESEL
	SEMI-AUTO
	}
	class Vehicle{
	-ID : String
	-Plate : String
	-Make : String
	-Model : String
	-Transmission : Enum
	-Engine : Enum
	-Year : String
	+register()
	}
	class Owner{
	-ID : String
	-Name : String
	-PhoneNo : String
	}
	class Booking{
    -ID : String
    -Plate : String
    -OwnerID : String
    -Cost : Decimal
    -Time : String
    -Description : String
    -Date : Date
    -Cost : Decimal
    }
    class Trainee{
    -ID : String
    -Name : String
    -PhoneNo : String
    -Email : String
    }
    class User{
    -username : String
    -password : String
    -email : String
    }
    class TrainingClass{
    -code : String
    -ID : Volunteer
    -name : String
    -type : Enum
    -date : Date
}
	