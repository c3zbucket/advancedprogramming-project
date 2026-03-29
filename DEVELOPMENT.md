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

For example - `Booking` requires a vehicle registration plate. Initially this was simply assigned as a string - a future problem was identified where atomicity would not be as enforced as a registration plate could be entered without a `Vehicle` entry it came from. Therefore introducing the scenario of mismatched or even no `Vehicle` entry being present with the entered plate. 

Leading to the correction below, where rather than a constant string holding the reg plate of the booking, an entire `Vehicle` project is held. Ergo, **dependency injection** can be implemented where a booking requires a previously registered `BookedVehicle` Vehicle record or a new one. 

````mermaid
classDiagram
    class Booking{
        -ID : String
        -BookedVehicle : Vehicle
        -Owner : Visitor
        -Cost : Decimal
        -Time : String
        -Description : String
        -Date : Date
        -Cost : Decimal
        }
```` 

Additionally, as seen above -  the owner ID associated with the booking has been changed to an instance of the new generic `Visitor` type that will encompass motorists that visit GMMW for any reason. Eliminating the need for separate `Trainee` and `Owner` classes. 

````mermaid
classDiagram
    class Visitor {
        -ID : String
        -Name : String
        -phoneNo : String
        -email : String
    }
```` 
Finally, an additional `Repair` class was created to allow for greater granularity in tracking what was done in a booking. `Repair` required a booking to be created that it was linked to. This way, bookings that involved multiple individual repairs could be listed, satisfying the condition of them being able to be 'drilled down' to in the interface. Parts would be stored in a strictly defined **list** for them, meaning a repair can use multiple parts. The cost will then calculate the total of the individual cost of each item.

Leading to a relationship between `Booking`, `Repair` and `Part` as shown

````mermaid
classDiagram
    Booking "*..1" *--o "0..*" Repair
    Repair "*..1" <--* "1..*" Part
    Vehicle "*..1" *..> "1..*" Booking
    class Vehicle{
        -id : String
        -plate : String
        -make : String
        -model : String
        -transmission : Enum
        -fuelType : Enum
        -year : String
    }
    class Booking{
        -id : String
        -bookedVehicle : Vehicle
        -owner : Visitor
        -cost : Decimal
        -time : String
        -description : String
        -date : Date
        -cost : Decimal
    }
    class Repair {
        -ascBooking : Booking
        -Description : String
        -parts : List~Part~
        -cost : Decimal
    }
    class Part {
        -id : String
        -make : String
        -type : Enum
        -cost : Decimal
        -date : Date
    }
```` 
### Final Diagram

With all the amendments implemented that are specified above - the final diagram composed is shown below:

````mermaid
classDiagram
    class Volunteer{
        <<Interface>>
        -id : String
        -name : String
        -phoneNo : String
        -email : String
        +updateRecord() Integer, String
    }
    class Student{
    }
    class Lecturer{
    }
    class Visitor {
        -id : String
        -Name : String
        -phoneNo : String
        -email : String
    }
    class ClassType {
        <<Enumeration>>
        MAINTENANCE
        ADVANCED REPAIR
        RESTORATION & CARE
    }
    ClassType --> "1..1" TrainingClass
    class TrainingClass{
        -code : String
        -ID : String
        -name : String
        -type : ClassType
        -attendees : List~Visitor~
        -date : Date
        +register() String
        +remove() String
        +reschedule() String
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
        ELECTRIC
    }
    class PartType {
        <<Enumeration>>
        ENGINE
        ELECTRICAL
        SUSPENSION
        DRIVETRAIN
        BODY
    }
    Visitor "*..1" *--o "1..*" Vehicle
    Transmission --> "1..1" Vehicle
    Engine --> "1..1" Vehicle
    class Vehicle{
        -id : String
        -owner : Visitor
        -plate : String
        -make : String
        -model : String
        -transmission : Enum
        -engine : Enum
        -year : String
        +ToString() String
    }
    Vehicle "*..1" *..> "1..*" Booking
    class Booking{
        -id : String
        -bookedVehicle : Vehicle
        -owner : Visitor
        -cost : Decimal
        -time : String
        -description : String
        -repairs : List<Repair>
        -date : Date
        -totalLab : Decimal
        -totalParts : Decimal
        -totalCost : Decimal
        +addRepair() void
        +removeRepair() void
        +updateTotal() void
        +remove() bool
        +ToString() String
    }
    Booking "*..1" *--o "0..*" Repair
    class Repair {
        -ascBooking : Booking
        -description : String
        -repairer : Volunteer
        -parts : List~Part~
        -partsCost : Decimal
        -totalCost : Decimal
        -labCost : Decimal
        +removePart() Decimal
        +addPart() Decimal
        +updateLab() void
        +updateCost() void
        +ToString() String
    }
    Repair "*..1" <--* "1..*" Part
    PartType --> "1..1" Part
    class Part {
        -id : String
        -make : String
        -type : Enum
        -cost : Decimal
    }
````



## Flowcharts
