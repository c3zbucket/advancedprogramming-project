namespace GMMWSystem;

/**
 * Abstract class containing core logic for most records stored in the system to inherit
 * Includes everything from bookings and repairs to training classes and vehicles
 */
public abstract class Record<T1, T2>
{
    // Unique ID identifier for the record
    public abstract string ID { get; }

    // Abstract implementation of ID generation for records needing 1 parameter
    public abstract string IDGen(T1 t1, T2 t2);
    
    // Abstract implementation of ID generation for records needing 2 parameters
    //public string IDGen();
    //public abstract string IDGen(O1 a, O2 b);
    
    // Abstract implementation of record retrieval for records
    public abstract string ToString();
}