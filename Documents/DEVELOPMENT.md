## Contents
<!-- TOC -->
* [**Design**](#design-)
  * [Contents](#contents)
  * [UML Diagram](#uml-diagram)
  * [Flowcharts](#flowcharts)
* [**Implementation**](#implementation)
  * [**Console**](#console)
<!-- TOC -->

# **Design** 

The first step of the process involved designing the solution. 

Alongside it being specified, an Object-Oriented approach was chosen as it allowed for the principles of SOLID () to be more stringently enforced. 
Most notably - the Liskov Substitution and Dependency Inversion Principles.

## [UML Diagram](UML.md)

## [Flowcharts](FLOWCHART.md)

---
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

This can be greatly simplified and made to be done every time the total cost property is accessed like so:

```csharp
private decimal TotalCost { get => partsCost + labCost;}
```

---
#### String formatted display

Overidden `ToString()` methods were declared for each record object to provide a convenient way to access a formatted string representation of an entry, such as below for `TrainingClass` objects

```csharp
    public override string ToString() =>
        $"Class ID: [{ID}] | Name: {Name} | Type: {ClassType} | Date: {Date:dd/MM/yyyy HH:mm} | Taught by: {student.Name} | Attendees: {attendees.Count} | Description: {Description}";
```

For handling the basic output of linked objects in collections, helper methods were created that utilised the `ToString()` methods to display details of every entry

For instance, in the `Booking` class when viewing repairs linked to bookings, the method `LinkedRepairs()` was initially conceptualised to solve the problem:

```csharp
    public List<string> LinkedRepairs()
    {
        List<string> repairList = new();
        foreach (Repair rep in Repairs ) { repairList.Add(rep.ToString()); }
        return repairList;
    }
```

Later improved using the `List.ForEach()` method to directly add every repair object in the list to a string list to be output. With the method being called at the end in the class's return statement in `ToString()` as so:
```csharp
    public override string ToString()
    {
        return $"Booking ID: [{ID}] \n Date: [{Date:dd/MM/yyyy} {Time:hh:mm}] \n Plate: {bookedVehicle.Plate.Trim()} \n Vehicle: {bookedVehicle.Make} {bookedVehicle.Model} \n Owner: {bookedVehicle.owner.Name} \n Repairs done: {LinkedRepairs()} | Description: {Description}";
    }
```

This didn't work as during runtime it would return the object reference rather than the `ToString` representation of each repair object

![[image-1.png]]

Using `String.Join` to append all the entries to a string was explored initially, this would be declared in Booking's `ToString()` method. However, this required additional logic as now you needed both the `LinkedRepairs` method to output a string-formatted list of linked repairs and the String.Join when returning a `Booking` object string. 

Eventually it was discovered that the `StringBuilder` class offered similar functionality with only two lines of code. This could be minimised to a single line excluding the declaration of a new `StringBuilder` object by using it in tandem with `List.ForEach()` like below:
```csharp
StringBuilder repairList = new();
Repairs.ForEach(repair => repairList.AppendLine(repair.ToString())); 
```

The `StringBuilder` object would now format each object to the class's own implementation of `ToString` on each new line thanks to `.AppendLine()` which skips the requirement of a newline argument such as `/n` or `Environment.NewLine` needing to be declared as well. Leading to a finalised form as below:

```csharp
public string LinkedRepairs()
{
	StringBuilder repairList = new();
	Repairs.ForEach(repair => repairList.AppendLine(repair.ToString())); 
	return repairList.ToString();
}
```

Later on, a check was added to output if the linked repair list was empty at the very beginning, which would immediately return a message stating it was and not initialise the `StringBuilder` object.

Back to ID generation - A problem realised later on was that the display of IDs generated using fields such as license plates (for example - `Booking` IDs) were created with spaces, which was not what was envisioned.

![[image-2.png]]


At first `.Remove()` from the String library was thought up as a solution. This allowed for the removal of white-space anywhere given an argument of which position to start from and the amount to remove by. 

Since it was a String method to, it could be embedded within the `.AppendFormat()` call to the ID generating StringBuilder.

Ergo, with the my current booking ID generated string looking like this:

`BK-AB12 FCD1004`

The logic required to remove the space between *2* and and *F* would resemble something like this:

```csharp
StringBuilder id = new();
id.AppendFormat($"BK-{vehicle.Plate.Remove(7,1)}{date:ddMM}");
return id.ToString();
```

Starting from index 7 which with 0-indexing would be the white-space space after 7th character - *2*

Ideally as a result it would have produced a new string like this:

`BK-AB12FCD1004`

However it did not, instead removing the first character at the 2nd part of the license plate sub-string, which in this example would be removing F instead of the space, producing a string like this instead, preserving the white-space:

![[image-3.png]]

The fatal error was not realised until an hour later. Which being with `.Remove()` being called *within* the license plate subs-string, it was going to calculate the start of the 'index' from there rather than the string being produced for the StringBuilder. 

Therefore the starting index should have been 4, which when changed to that, correctly removed the white-space:
![[image-4.png]]
### Initial Testing 

For initial testing of functionality a back-box approach was followed where there would be different methods for both object instantiation and modification to them in accordance to the basic application requirements.

Following this produced successful results for the 4 basic requirements.

### Entity Framework Integration