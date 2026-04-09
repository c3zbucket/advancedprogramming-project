# **Design** 

## Contents
<!-- TOC -->
* [**Design**](#design-)
  * [Contents](#contents)
  * [UML Diagram](#uml-diagram)
  * [Flowcharts](#flowcharts)
* [**Implementation**](#implementation)
  * [**Console**](#console)
<!-- TOC -->


The first step of the process involved designing the solution. 

Alongside it being specified, an Object-Oriented approach was chosen as it allowed for the principles of SOLID () to be more stringently enforced. 
Most notably - the Liskov Substitution and Dependency Inversion Principles.

## [UML Diagram](UML.md)

## [Flowcharts](FLOWCHART.md)

# **Implementation**

For a basic implementation, it was decided to start with a console app demonstrating the core functionality including **ORM mapping** via .NET's **Entity Framework**. This was done so the foundation of the application would be resolute. 

## **Console**

### Initialising Class Structure

The first step was translating the UML defined structure to C# classes. In the midst of performing this, an improvement was identified with the manner in which the `Motorist` class was implemented. Initially despite being basically identical in contents of fields to implementations of the `IStaff`  class such as `Student` and `Lecturer`, it was declared as a separate class. 

This was then changed to be an implementation of a new general `IPerson`  interface. Whilst the `Student` and `Lecturer` classes were still implementations of `IStaff`, now a sub-interface of `IPerson`. Thus allowing for stronger **interface segregation** - a core principle of SOLID. (citation).

With the fields filled - came the next step of adding basic methods that would manipulate the properties. 

From here it was found that the general field contents of classes being utilised as objects for the the so called 'Menu functions' would almost always contain a unique ID at the start. Alongside this basic requirements of the brief and with how some of the objects, mainly `Booking` and `TrainingClass`  contain collections as fields to store variable-amount of data and so would require methods to modify those collections. In the case of Booking it would be adding repairs which are important as the total and final cost to the customer would be calculated from these. 

Therefore necessitating the existence of an `IRecord` interface to maintain strong adherence to SOLID principles which in-turn would additionally aid when expanding the application.

This was changed to a `Record` abstract class so very commonly used fields like `ID` and `Time` could also be declared for child classes to inherit from.

```csharp
/**
 * Abstract class containing core logic for most records stored in the system to inherit
 * Includes everything from bookings and repairs to training classes and vehicles
 */
public abstract class Record
{
    // Unique ID identifier for the record
    public string id;
	
    //DateTime object used to store date and time for the record
    public DateTime time;
    
    // Abstract implementation of ID generation for records
    public abstract string IDGen();

    // Abstract implementation of record retrieval for records
    public abstract Record GetRecord();

    // Abstract implementation of adding to collection fields 
    public abstract Record Add();
    
    // Abstract implementation of oving from collection fields
    public abstract Record Remove();
}
```

The `IDGen` method would take two defined objects to generate a suitable ID with them.

This was amended further to only keep the ID and IDGen methods as those were being employed the most across the 'function' classes.

To fix the problem of handling the various datatypes used in the IDGen - **generic types** (source) were employed as the best solution to maintain strong type-safety while allowing for any object type to be used by the inheriting classes' `IDGen` methods.

When implementing the `Part` class, the `used` field was dropped as that is already tracked by the `Repair` class.

When implementing the repair calculation logic, an improvement in the logic of totalCost through **expression bodying** (source)

Before, the property would be recalculated everytime it was changed: for example, labour costs being manually modified. After which it would calculate the new one with the new labour cost and current part costs by calling an implemented `recalc()` method:

```csharp
public decimal recalc()=> totalCost = labCost + partsCost;
```

Overidden `ToString()` methods were declared for each record object to provide a convenient way to access a formatted string representation of an entry, such as below for `TrainingClass` objects

```csharp
    public override string ToString() =>
        $"Class ID: [{ID}] | Name: {Name} | Type: {ClassType} | Date: {Date:dd/MM/yyyy HH:mm} | Taught by: {student.Name} | Attendees: {attendees.Count} | Description: {Description}";
```

For handling the basic output of linked objects in collections, helper methods were created that utilised the `ToString()` methods to display details of every entry

For instance, in the `Booking` class when viewing repairs linked to bookings, the method ``
### Initial Testing

After restructuring the logic in the methods with 