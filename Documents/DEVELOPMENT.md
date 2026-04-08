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


