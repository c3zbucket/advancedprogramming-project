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

The next step was laying out the migration to Entity Framework which would automate the process of object-relational mapping.

For best practice, a new branch - `ef-dev` was created which would contain the development and integration of EF until it's satisfactory feature-complete according to the specifications and safe to be merged to the main branch.

Before starting, an environment that could support EFCore - the version of EF used was required, therefore requiring additional nuGet packages to add. These were added with ease using the .NET CLI using the `dotnet add package` command. From this the following two packages were added to the application:

```csharp
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

To start, an implementation of the DBContext class was required, this is where `GMMWContext` comes in where the connection to EF and the properties are declared. 

Next was creating a JSON configuration file for initialising the database connection. This was initially created manually and placed in the solution root, which was a mistake realised when creating the migration and it produced an error that it could not find the JSON file. 

![[image-5.png]]

The solution was to use Rider's tool through the project context menu, which placed it in the correct directory EF expects it in.

After doing so, I encountered another error where the migration could not properly map properties with the existing constructor.


![[image-7.png]]

One solution proposed on this StackOverflow thread suggested simply declaring a blank constructor - https://stackoverflow.com/a/65179920, however upon further research that is not considered good practice or ideal with EF's database generation. This lead to a re-approach to class structure where I restructured the record-based classes as 'POCO' entities (https://learn.microsoft.com/en-us/previous-versions/dotnet/netframework-4.0/dd456853(v=vs.100)?redirectedfrom=MSDN) that simply contained properties with their accessors, any additional logic linked to the classes would be in separate classes that would be called later on.

After the process, including revising the `GMMWMenu` class to now be the new static entry point, the migration executed successfuly:

![[image-8.png|847]]
Subsequently, updating the database also successfully executed the generated creation code

The next step was to test the display of these. This was tested using the revised `GMMWMenu` using a simple number-based menu system. The input of a number would bring you to the corresponding function that would display the respective entities. To fill the database, the previous Testing class's instantiation logic was reused and reworked.

Initially, to access the data stored in the mapped database, methods such as simple `foreach` loops were utilised to iterate over every entries in the database through opening a new 'connection' through the implementation of the DbContext class, like below:

```csharp
    private static void MotoristMenu()
    {
        using var context = new GMMWDBContext();
        var motorists = context.Motorists.ToList();
        Console.WriteLine("\n--- Registered Motorists ---");
        foreach (var m in motorists)
        {
            Console.WriteLine($"ID: {m.ID} | Name: {m.Name} | Phone: {m.PhoneNo} | Email: {m.Email}");
        }
    }
```

This was relatively simple and effective for meeting the basic requirements until it came to implementing the more complicated ones - specifically centring around entries with linked objects for fields such as Bookings or T. This is where LINQ began to be utilised as a querying language in the form of lambda-styled expressions. Which for meeting the initial requirements of basic display was able to be completed with relative ease. `.Include` was utilised similar to a join from SQL where 'sub-queries' where included to additionally display details relating to connected objects.

One problem encountered was for the display of 'staff' at the workshop as EF did not accept a DbSet created with the `IStaff` interface as a type as it violated the usage of an interface, producing the following exception on runtime:

![[image-9.png]]

The solution as deducted from this [StackOverflow thread]([EF .net6 Migration "must be a non-interface reference type to be used as an entity type."](https://stackoverflow.com/questions/73479568/ef-net6-migration-must-be-a-non-interface-reference-type-to-be-used-as-an-enti)) required for the `IStaff` child interface of the general `IPerson` interface which all people objects implement to be an abstract class instead declaring the common fields for both Students and Lecturer employee records.

After which I got the error of 'circular dependencies' - which was hypothesised being from only writing the changes at the very end of the debug method when every table was created. Changing the method to save after every table creation fixed this.

This lead to me also discovering that I had made a fatal flaw with the People class structure - since `SystemUser` had a dependency on `Staff` since it composed of a `Member` object that would link the account to an existing employee record, 'dedicated' Admins - which as the brief stated could be people who are not volunteers such as students or lecturers but rather individuals who are solely there for the administration of the system at the workshop, could not create accounts. This was fixed by adding a dedicated `Admin` class which inherited from the `Staff` abstract class.

Subsequently, to address the issues with `SystemUser` entries in the table identifying staff IDs as foreign keys, a dedicated `ID` property was added for it  through which`OnModelCreating` method was called to manually specify the relationship, therefore leading to the instantiation requiring the linked Staff object's ID to be declared as well.

Thus finally producing functioning display of the stored records in the database.
## **Blazor Web**

With basic functionality now implemented with functioning EF ORM mapping, it was time to begin the transition to Blazor Web. The finalised EF codebase can be merged to main branch. The blazor-dev branch was then subsequently created 

Initially it was thought of creating it as a project linked to the existing GMMWSystem class, sharing the same solution file. This was done through the context menu in Rider rather than the 'Create new Project' which allowed you to create a linked project without a `.sln` solution file
![[image-11.png]]

After going with the approach it was realised in the long-term it would be hard to maintain with the standard template Blazor offering you being equipped to store POCO classes in a *Models* - in this case called: *Records*. So it was decided that a new project would be created and the code required from the previous one would be manually migrated, which with search and replace tools in Blazor, was manageable.

The records classes were stored under the *Data* directory that came with Blazor.

Then came migrating the record classes and making the needed adjustments alongside the core logic migrated to their respective `Services` classes 

On the first debug everything seemed fine, until an entry in the navmenu was replaced to link to a staff management page, which when clicked would simply hang - stating that there was an error:
![[image-13.png]]



The issue identified was with the blazor server still relying on the default `AppDbContext` file rather than the `GMMWDBContext` one I wanted to replace it with. This would be initially triggered on startup in the `Program.cs`  file in the root directory:

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));
```

This was changed to GWWDBContext, alongside the `AddDbContextFactory` method rather than the standard `AddDbContext` to automate a more efficient implementation of DB contexts.

```csharp
builder.Services.AddDbContextFactory<GMMWDBContext>(options =>
    options.UseSqlite(connectionString));
```

After applying those changes the error still persisted - turns out that actual underlying issue was relying on a static settings json file for initialising the connection for the context factory rather than relying on dependency injection from the `Program.cs` file which was already created on startup to handle it:

```csharp title:Program.cs

// Initialise DB connection, return exception if not found
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string not found.");

// Create context factory from this
builder.Services.AddDbContextFactory<GMMWDBContext>(options =>
    options.UseSqlite(connectionString));

```

Leading to the duplicate logic not needed in the `OnConfiguring` method, with the new `OnConfiguring` implementation being below:

```csharp title:GMMWDBContext
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if not already configured by DI from Program.cs
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                // Enable verbose logging for debug - remember remove in prod branch
                .EnableSensitiveDataLogging()
                .LogTo(x => Debug.WriteLine(x));
        }
    }
```

To access this, the db context class would take a constructor with the context and associated options passed as parameters.

```csharp
public GMMWDBContext(DbContextOptions<GMMWDBContext> options) : base(options) { }
```

After adjusting this, the Staff page finally loaded with the table event to store staff entries:
![[image-14.png|708]]

To test for the display of added records, `.HasData` in the debug context class was used for a quick and simple way to add entries to the database.

```csharp title:GMMWDBContext
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemUser>().HasDiscriminator().IsComplete(false);

        modelBuilder.Entity<SystemUser>()
            .HasOne(u => u.Member)
            .WithOne()
            .HasForeignKey<SystemUser>(u => u.ID);

        // Add debug data for Staff (Students and lecturers)
        modelBuilder.Entity<Student>().HasData(
            new Student { ID = "S001", Name = "Oliver Smith", PhoneNo = "07700900001", Email = "o.smith@outlook.com" },
            .....
            }
```

With a new migration created to test the addition of this safely.

Thus leading to functioning output of staff entries:

![[image-15.png|844]]

With the basic display nailed down - it had to be done to the other pages before moving onto the more advanced requirements utilising LINQ querying for filtering or searching.

On the way - `Vehicle`'s primary key was decided to be changed to it's registration plate - `Plate` as the additional `VehicleID` was found redundant. When updating the database however to reflect this change after already defining `.HasKey`  in its model creation for EF - it returned an error that an 'ID' was null:
![[image-16.png]]

After investigating, even after declaring the `Plate` property with the `[Key]` attribute above it additionally as stated [in this StackOverflow answer](https://stackoverflow.com/questions/13607512/how-to-specify-primary-key-name-in-ef-code-first#13612347) , it still returned errors and expected an `ID` field in the schema. It was at this point this was abandoned and instead a `Display` attribute was declared so in output to the user through the use of a class that obtains the `DisplayName` of the property - the 'ID' would show as the 'registration plate' while logically it would still be referred to as a `Vehicle` object's ID like below:

```csharp
<table>
	<thead>
	<tr>
		<th>
			<DisplayName For="@(() => _vehicleList.FirstOrDefault().ID)"/>
		</th>
		<th> <DisplayName For="@(() => _vehicleList.FirstOrDefault().owner)"/> </th>
		<th>Transmission</th>
		//... Other properties..
	</tr>
	</thead>
	<tbody>
	        @foreach (var v in _vehicleList)
        {
            <tr>
                <td>@v.ID</td>
                <td>@v.owner.Name</td>
				//... Other properties..
            </tr>
        }
	</tbody>
</table>
```

Leading to an output on its page like so:
![[image-17.png]]

No issues were encountered until approaching the `Repairs` table. Here there are multiple list types for properties including `repairers` and `parts` . This would be fine in creation normally - declaring them as 'navigation properties', but utilising `.HasData` via the context class did not allow this. Therefore this was left for when implementing the input form.

### Taking Input

With basic display fully functioning, the next step was to implement a working system to take the input of new entries to the records from users. The most ideal solution that was decided on was Microsoft's own *EditForm* class.

The motorist form was created first as that has no 'dependencies' with it's properties.

A basic form was created in the class `NewMotorist` which on a valid submission would call an async `Task` method which would call the injected service class to add a motorist entry.

The next step was to add necessary validation - this was especially important for phone numbers which must be strings containing 11 characters that are exclusively digits and begin with 07 usually. 

Initially, this was thought of simply using `inputNumber` to immediately parse the input as an integer, however on deeper research it was realised that `inputNumber` values drop the leading zero, additionally it would not be very ideal to have the motorist service class parse the the input to a stringbuilder to add a leading zero as well as perform the strength validation when utilising the field's setter.

With existing familiarity with regex it was then thought that were must be a constraint that allowed for inputs to be compared to this and after some research it was found there was using the `RegularExpression` attribute - leading to a declaration like below in the Motorist record class. Where it would return an error if the input number did not match this regex string.

```csharp
    // Entered phone number must be 11 digits and begin with 01/2 for landline or 07 for mobile phone
    [RegularExpression(@"^0[127]\d{9}$", ErrorMessage = "Phone number be valid UK phone number")]
    public string PhoneNo { get; set; }

```

This was similarly applied to the email field to ensure only valid emails were entered.

Finally leading to an input form like this with valid validation in each field:

![[image-19.png]]

Additionally it was decided to implement logic to the button so it would be un-highlighted and be un-clickable until all fields were filled with valid information. This proved harder than initially thought as it wasn't as simple as using *EditContext* and checking the state of the form via *StateChange*. After research on the topic on checking the state of the model to rerun validation whenever the change in state of *EditContext* from user input was detected as discussed [here](https://stackoverflow.com/questions/29309803/asp-net-mvc-modelstate-how-to-re-run-validation#29316061). 

From this an implementation arose where the current state of the *EditContext* was checked using the object's own `Validate` method and alerting the form about the state of the change - which was declared on the creation of the submission button utilising the standard *disabled* html rule with the `@` to trigger the execution of the 'checker' method.

(use uuid for IDs and authentication)

Logically, the code resembled something like this:
```csharp
editContext.OnValidationStateChanged += (_, _) => // Call method with blank messages to disregard required parameters
{
    validField = editContext.Validate();  // Validate current editContext status
    StateHasChanged(); // Alert that state has changed
};
```

When implemented, this did not work and instead produced a `StackOverflow` exception which was realised from `.Validate()` essentially being called an infinite number of times everytime the state of `EditContext()` changed.

The fix was to instead read if any validation messages had been producing during the current edit context and notify the targeted component which would change its state live. This was then added to an overridden `OnInitialized` method to check for this and reset the validation check whenever a change was detected in the current edit context. Which would in turn update the status of the `disabled` tag in the button element - essentially leading to a functioning implementation as shown below:

 ![[image-21.png|269]]
With invalid inputs - the button would be greyed out and un-clickable. 

![[image-22.png|281]]
With valid ones - the button would be highlighted and thus clickable.

Thus leading to a new record being added:
![[image-24.png|465]]

From this template it was relatively intuitive to adapt this for the other records.