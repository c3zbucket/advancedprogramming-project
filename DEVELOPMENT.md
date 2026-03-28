# Design 

The first step of the process involved designing the solution. 

Alongside it being specified, an Object-Oriented approach was chosen as it allowed for the principles of SOLID () to be more stringently enforced.

Most notably - the Liskov Substitution and Dependency Inversion Principles.

## UML Diagram

The first step was to create a UML Diagram of the solution. 
Mermaid was chosen as it allowed for the creation of such a diagram through an intuitive markdown-based language. It also had an integration with Rider.

Firstly - the classes of the 'entities' in this case needed to be identified.

I conceptualised a basic layout without relationships based on my ERD of the scenario last year.

<!--- Class Diagram for GMMR Systems -->

````mermaid
---
title: Basic Layout
---
classDiagram
	class Volunteer{
	-id : String
	-name : String
	-phoneNo : String
	-email : String
	}
	class Vehicle{
	-id : String
	-plate : String
	-make : String
	-model : String
	-transmission : Enum
	-fuelType : Enum
	-year : String
	+register()
	}
	class Owner{
	-id : String
	-name : String
	-phoneNo : String
	}
	class Booking{
    -id : String
    -plate : String
    -ownerID : String
    -cost : Decimal
    -time : String
    -description : String
    -date : Date
    -cost : Decimal
    }
    class Trainee{
    -id : String
    -name : String
    -phoneNo : String
    -email : String
    }
    class User{
    -username : String
    -password : String
    -email : String
    }
    class TrainingClass{
    -code : String
    -ID : String
    -name : String
    -type : String
    -date : Date
    }
    class Part {
    -id : String
    -make : String
    -type : Enum
    -cost : Decimal
    -date : Date
    }
````

As this concept developed, multiple changes were made from the classic previous ERD diagram 

### Handling 'Foreign' Values

Lookup tables created were dropped from this implementation as the logic with shared attributes that otherwise would be 'foreign keys' in other classes would be abstracted in the SQL implementation.

### Changing Data Types

Some entities were intended to hold a preset of values. One example would be the property `Transmission` which stored the transmission of the car.
This can have the values of - `Automatic`, `Manual` or `Semi-Auto`. For this reason, such properties when translated to attributes in the class diagram were chosen to be **enumerations** to define a strict set of constants that transmissions can be, nothing else.

In UML class diagrams an Enum is declared with the tags of `<<Enumeration>>`; thus leading to the additional classes for `Transmission`, FuelType, now named `Engine` and `Type` for `type` in `TrainingClass`

````mermaid
classDiagram
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
    class Type {
        <<Enumeration>>
        MAINTENANCE
        ADVANCED REPAIR
        RESTORATION & CARE
    }
````

### Defining Custom Types

For further enforcement of SOLID principles we can define a data type from a class.


## Flowcharts
