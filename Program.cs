using System.Runtime.CompilerServices;
using System.Xml.Linq;
using System.Linq;

namespace HotalSystem
{
    internal class Program
    {
        //global storage
        
        // This object is used globally in the Program class to manage guests, rooms, and bookings
        static Hotel hotel = new Hotel("Grand Azure");

        //static List<Guest> guests = new List<Guest>();
        //static List<Room> rooms = new List<Room>();
        //static List<Booking> bookings = new List<Booking>();
        //static List<Hotel> hotels = new List<Hotel>();



        //helping functions
        static public void seed()
        {
            // seed data for guest
            hotel.AddGuest("Ali", "A123");
            hotel.AddGuest("Sara", "A555");
            hotel.AddGuest("Ahmed", "B100");

            // seed data for rooms
            hotel.AddRoom(25, "Standard");
            hotel.AddRoom(32, "Deluxe");
            hotel.AddRoom(80, "Suite");

            // seed data for bookings
            hotel.BookRoom("A123", 25);
            hotel.BookRoom("A555", 32);
            hotel.BookRoom("B100", 80);
        }
        static public void displayMenue()
        {
            Console.WriteLine("===== Grand Azure Hotel System =====");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("1. Add Guest");
            Console.WriteLine("2. Add Room");
            Console.WriteLine("3. Book a Room");
            Console.WriteLine("4. Cancel Booking");
            Console.WriteLine("5. Display Available Rooms");
            Console.WriteLine("6. Display Booked Rooms");
            Console.WriteLine("7. Search Guest by National ID");
            Console.WriteLine("8. Show Hotel Statistics");
            Console.WriteLine("9. Exit");

        }
       public  static void AddGuest()
        {
            Console.Write("Enter Guest Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter National ID: ");
            string id = Console.ReadLine();

            hotel.AddGuest(name, id);

            Console.WriteLine("Guest added successfully.");
        }
        static public bool ConfirmExit()
        {
            Console.WriteLine("Are you sure you want to exit? (yes/no)");
            string confirm = Console.ReadLine()?.Trim().ToLower();

            if (confirm == "yes")
            {
                Console.WriteLine("Exiting system...");
                Console.WriteLine("Thank you for using the Healthcare Management System!");
                Console.WriteLine("----------------------------------------");
                // user confirmed exit
                return true; 
            }
            else
            {
                Console.WriteLine("Exit cancelled. Returning to menu...");
                // user canceled exit
                return false; 
            }

        }
       public  static void AddRoom()
        {
            Console.Write("Enter Room Number: ");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            Console.Write("Enter Room Type: ");
            string type = Console.ReadLine();

            hotel.AddRoom(number, type);

            Console.WriteLine("Room added successfully.");
        }
        public static void BookRoom()
        {
            Console.Write("Enter Guest National ID: ");
            string id = Console.ReadLine();

            Console.Write("Enter Room Number: ");

            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            hotel.BookRoom(id, roomNumber);
        }
        public static void CancelBooking()
        {
            Console.Write("Enter Booking ID to cancel: ");

            if (!int.TryParse(Console.ReadLine(), out int bookingID))
            {
                Console.WriteLine("Invalid Booking ID.");
                return;
            }

            hotel.CancelBooking(bookingID);
        }
        public static void DisplayAvailableRooms()
        {
            hotel.DisplayAvailableRooms();
        }
        public static void DisplayBookedRooms()
        {
            hotel.DisplayBookedRooms();
        }
        public static void SearchGuestByNationalID()
        {
            Console.Write("Enter National ID: ");
            string id = Console.ReadLine();

            Guest guest = hotel.FindGuest(id);

            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            Console.WriteLine("Guest found:");
            guest.DisplayInfo();
        }
        public static void ShowHotelStatistics()
        {
            hotel.DisplayStatistics();
        }
        static void Main(string[] args)
        {
            //seed();
            bool exit = false;

            while (!exit)
            {
                displayMenue();

                int option;

                Console.Write("Choose option: ");

                // safe input handling
                if (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 9.");
                    continue;
                }

                //oprtaional functions
                switch (option)
                {
                    case 1://add guest
                        AddGuest();
                        break;
                    case 2://add room
                        AddRoom();
                        break;
                    case 3://book room
                        BookRoom();
                        break;
                    case 4://cancel booking
                        CancelBooking();
                        break;
                    case 5://display avaiable rooms
                        DisplayAvailableRooms();
                        break;
                    case 6://display booked rooms
                        DisplayBookedRooms();
                        break;
                    case 7://search guest by national id
                        SearchGuestByNationalID();
                        break;
                    case 8://show hotel statistics
                        ShowHotelStatistics();
                        break;
                    case 9://Exit
                        if (ConfirmExit())
                        {
                            exit = true;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 9.");
                        break;

                }
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();


            }
        }
    }
}




/// /////////////Guest class//////////////////////////////////

public class Guest
{
    // Fields
    private static int totalGuestsCreated = 0;
    private string nationalID;
    private string fullName;

   

        // Constructor
        public Guest(string name, string id)
        {
           
            FullName = name;

            // Validate ID
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("National ID cannot be null or empty.");

            nationalID = id;

            // Increment counter
            totalGuestsCreated++;
        }

        // Read-only property
        public string NationalID
        {
            get { return nationalID; }
        }

        // Property with validation
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Full name cannot be null or empty.");

                fullName = value;
            }
        }

        // Static method
        public static int GetTotalGuestsCreated()
        {
            return totalGuestsCreated;
        }

        // Instance method
        public void DisplayInfo()
        {
            Console.WriteLine("Guest Name: " + FullName);
            Console.WriteLine("National ID: " + NationalID);
        }
    }


/// /////////////Room class//////////////////////////////////
public class Room
{
    // Fields
    private int roomNumber;
    private string roomType;
    private bool isBooked;

   
        // Constructor
        public Room(int number, string type)
        {
            if (number <= 0)
                throw new ArgumentException("Room number must be positive.");

            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentException("Room type cannot be null or empty.");

            roomNumber = number;
            roomType = type;
            isBooked = false;
        }

        // Properties (get only)
        public int RoomNumber
        {
            get { 
            return roomNumber;
        }
        }

        public string RoomType
        {
            get {
            return roomType;
        }
        }

        public bool IsBooked
        {
            get { 
            return isBooked;
        }
        }

        // Methods
        public bool Book()
        {
            if (isBooked)
                return false;

            isBooked = true;
            return true;
        }

        public void CancelBooking()
        {
            isBooked = false;
        }

        public void DisplayInfo()
        {
            Console.WriteLine("Room Number: " + roomNumber);
            Console.WriteLine("Room Type: " + roomType);
            Console.WriteLine("Status: " + (isBooked ? "Booked" : "Available"));
        }
    }


/// /////////////Booking class//////////////////////////////////
public class Booking
{
    // Fields
    private static int nextBookingID = 1;

    private int bookingID;
    private Guest guest;
    private Room room;

   

        // Constructor
        public Booking(Guest guest, Room room)
        {
            if (guest == null)
                throw new ArgumentNullException(nameof(guest));

            if (room == null)
                throw new ArgumentNullException(nameof(room));

            bookingID = nextBookingID++;
            this.guest = guest;
            this.room = room;
        }

        // Properties (get only)
        public int BookingID
        {
            get { 
            return bookingID;
        }
        }

        public Guest Guest
        {
            get { 
            return guest;
        }
        }

        public Room Room
        {
            get { 
            return room;
        }
        }

        // Method
        public void DisplayInfo()
        {
            Console.WriteLine("Booking ID: " + BookingID);
            Console.WriteLine("Guest Name: " + Guest.FullName);
            Console.WriteLine("Room Number: " + Room.RoomNumber);
            Console.WriteLine("Room Type: " + Room.RoomType);
        }
    }



/// /////////////Hotel class//////////////////////////////////

public class Hotel
{
    // Fields
    private List<Guest> guests;
    private List<Room> rooms;
    private List<Booking> bookings;

    // Property (get-only)
    public string HotelName { get; private set; }

    // Constructor
    public Hotel(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Hotel name cannot be empty.");

        HotelName = name;

        guests = new List<Guest>();
        rooms = new List<Room>();
        bookings = new List<Booking>();
    }

  
    public void AddGuest(string name, string id)
    {
        // Check if a guest with the same National ID already exists in the list
        // Any() returns true if at least one guest matches the condition
        if (guests.Any(g => g.NationalID == id))
        {
            Console.WriteLine("Guest with this ID already exists.");
            return;
        }

        guests.Add(new Guest(name, id));
    }

    public Guest FindGuest(string nationalID)
    {
        // Search the guests list for a Guest whose NationalID matches the given value
        // Find() returns the first matching Guest object or null if no match is found
        return guests.Find(g => g.NationalID == nationalID);
    }

   
    public void AddRoom(int number, string type)
    {
        if (rooms.Any(r => r.RoomNumber == number))
        {
            Console.WriteLine("Room already exists with this number.");
            return;
        }

        rooms.Add(new Room(number, type));
    }

    public void DisplayAvailableRooms()
    {
        Console.WriteLine("Available Rooms:");

        bool found = false;

        foreach (Room r in rooms)
        {
            if (!r.IsBooked)
            {
                r.DisplayInfo();
                Console.WriteLine("------------------------");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No available rooms.");
    }

    public void DisplayBookedRooms()
    {
        Console.WriteLine("Booked Rooms:");

        if (bookings.Count == 0)
        {
            Console.WriteLine("No bookings found.");
            return;
        }

        foreach (Booking b in bookings)
        {
            b.DisplayInfo();
            Console.WriteLine("------------------------");
        }
    }

  
    public void BookRoom(string nationalID, int roomNumber)
    {
        Guest guest = FindGuest(nationalID);

        if (guest == null)
        {
            Console.WriteLine("Guest not found.");
            return;
        }

        Room room = rooms.Find(r => r.RoomNumber == roomNumber);

        if (room == null)
        {
            Console.WriteLine("Room not found.");
            return;
        }

        if (!room.Book())
        {
            Console.WriteLine("Room is already booked.");
            return;
        }

        Booking booking = new Booking(guest, room);
        bookings.Add(booking);

        Console.WriteLine("Booking successful!");
        booking.DisplayInfo();
    }

    public void CancelBooking(int bookingID)
    {
        Booking booking = bookings.Find(b => b.BookingID == bookingID);

        if (booking == null)
        {
            Console.WriteLine("Booking not found.");
            return;
        }

        booking.Room.CancelBooking();

        bookings.RemoveAll(b => b.BookingID == bookingID);

        Console.WriteLine("Booking cancelled successfully.");
    }

    public void SearchGuestBookings(string nationalID)
    {
        Guest guest = FindGuest(nationalID);

        if (guest == null)
        {
            Console.WriteLine("Guest not found.");
            return;
        }

        Console.WriteLine($"Bookings for {guest.FullName}:");

        bool found = false;

        foreach (Booking b in bookings)
        {
            if (b.Guest.NationalID == nationalID)
            {
                b.DisplayInfo();
                Console.WriteLine("------------------------");
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No bookings found for this guest.");
    }

  
    public void DisplayStatistics()
    {
        Console.WriteLine("===== Hotel Statistics =====");

        Console.WriteLine("Hotel Name: " + HotelName);
        Console.WriteLine("Total Guests: " + guests.Count);
        Console.WriteLine("Total Rooms: " + rooms.Count);
        Console.WriteLine("Total Bookings: " + bookings.Count);

        // Count how many rooms are currently booked in case  (IsBooked == true)
        int bookedRooms = rooms.Count(r => r.IsBooked);
        // Calculate available rooms by subtracting booked rooms from total rooms
        int availableRooms = rooms.Count - bookedRooms;

        Console.WriteLine("Booked Rooms: " + bookedRooms);
        Console.WriteLine("Available Rooms: " + availableRooms);

        Console.WriteLine("Total Guests Ever Created: " + Guest.GetTotalGuestsCreated());

        Console.WriteLine("=============================");
    }
}

