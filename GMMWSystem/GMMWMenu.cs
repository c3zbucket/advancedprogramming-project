namespace GMMWSystem;

public class GMMWMenu
{
    public List<IStaff> staffList;
    
    public List<Motorist> motoristList;
    
    public List<Vehicle> vehicleList;
    
    public List<Booking> bookingsList;
    
    public List<Repair> repairsList;
    
    public List<Part> partsList;
    
    public List<TrainingClass> classesList;
    
    public  GMMWMenu() {}


    public static void main(String[] args)
    {
        GMMWMenu menu = new GMMWMenu();

        Student mek = new Student("2424", "Me me", "07562399573", "adfdaf@gmail.com");
    }
}