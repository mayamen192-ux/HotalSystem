using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            // seed data for guests
            hotel.AddGuest("Ali", "A123");
            hotel.AddGuest("Sara", "A555");
            hotel.AddGuest("Ahmed", "B100");

            // seed data for rooms (now requires nightly rate)
            hotel.AddRoom(25, "Standard", 100m);
            hotel.AddRoom(32, "Deluxe", 150m);
            hotel.AddRoom(80, "Suite", 300m);

            // seed data for bookings (now requires nights)
            hotel.BookRoom("A123", 25, 2);   // Ali books room 25 for 2 nights
            hotel.BookRoom("A555", 32, 3);   // Sara books room 32 for 3 nights
            hotel.BookRoom("B100", 80, 1);   // Ahmed books room 80 for 1 night
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
            Console.WriteLine("9. Filter Available Rooms by Type");
            Console.WriteLine("10.Display All Guests");
            Console.WriteLine("11.Most Expensive Active Booking");
            Console.WriteLine("12.Guest Life time Booking Counter");
            Console.WriteLine("13.Exit");

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
        public static void AddRoom()
        {
            Console.Write("Enter Room Number: ");
            if (!int.TryParse(Console.ReadLine(), out int number))
            {
                Console.WriteLine("Invalid room number.");
                return;
            }

            Console.Write("Enter Room Type: ");
            string type = Console.ReadLine();

            Console.Write("Enter Nightly Rate: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal rate))
            {
                Console.WriteLine("Invalid nightly rate.");
                return;
            }

            hotel.AddRoom(number, type, rate);
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

            Console.Write("Enter Number of Nights: ");
            if (!int.TryParse(Console.ReadLine(), out int nights))
            {
                Console.WriteLine("Invalid number of nights.");
                return;
            }

            hotel.BookRoom(id, roomNumber, nights);
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
        public static void FilterAvailableRoomsByType()
        {
            Console.Write("Enter Room Type (Single / Double / Suite): ");
            string type = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(type))
            {
                Console.WriteLine("Invalid room type.");
                return;
            }

            hotel.DisplayAvailableRoomsByType(type);
        }
        public static void DisplayAllGuests()
        {
            hotel.DisplayAllGuests();
        }
        public static void FindMostExpensiveBooking()
        {
            hotel.FindMostExpensiveBooking();
        }
        public static void GuestLifetimeBookingCounter()
        {
            Console.Write("Enter National ID: ");
            string id = Console.ReadLine();

            Guest guest = hotel.FindGuest(id);

            if (guest == null)
            {
                Console.WriteLine("Guest not found.");
                return;
            }

            guest.DisplayInfo();
            Console.WriteLine("Total bookings ever made: " + guest.TotalBookingsMade);
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
                    Console.WriteLine("Invalid input. Please enter a number from 1 to 12.");
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
                    case 9://Filter Available Rooms by Type
                        FilterAvailableRoomsByType();
                        break;
                    case 10://Display All Guests
                        DisplayAllGuests();
                        break;
                    case 11://Most ExpensiveActive Booking
                        FindMostExpensiveBooking();
                        break;
                    case 12://Guest Lifetime Booking Counter
                        GuestLifetimeBookingCounter();
                        break;                
                    case 13://Exit
                        if (ConfirmExit())
                        {
                            exit = true;
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose between 1 and 12.");
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
    private int totalBookingsMade = 0;


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
            get {
            return fullName; 
        }
            set
            {
           //update 1:
            if (string.IsNullOrWhiteSpace(value) ||value.Trim().Length < 3)
                Console.WriteLine("Name must be at least 3 chars.");
                

                                fullName = value;
            }
        }
    public int TotalBookingsMade
    {
        get { return totalBookingsMade; }
    }

    // Static method
    public static int GetTotalGuestsCreated()
        {
            return totalGuestsCreated;
        }
    public void IncrementBookingCount()
    {
        totalBookingsMade++;
    }

    // Instance method
    public void DisplayInfo()
        {
            Console.WriteLine("Guest Name:  " + FullName);
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
    private decimal nightlyRate;

    // Constructor
    public Room(int number, string type, decimal rate)
    {
        if (number <= 0)
            throw new ArgumentException("Room number must be positive.");

        if (string.IsNullOrWhiteSpace(type))
            throw new ArgumentException("Room type cannot be null or empty.");

        if (!IsValidType(type))
            throw new ArgumentException("Invalid room type.");

        if (rate <= 0)
            throw new ArgumentException("Nightly rate must be positive.");

        string norm = NormalizeType(type);

        roomNumber = number;
        roomType = norm;
        nightlyRate = rate;
        isBooked = false;
    }

    // Properties
    public int RoomNumber
    {
        get
        {
            return roomNumber;
        }
    }
    public string RoomType
    {
        get
        { return roomType; }
    }
    public bool IsBooked
    {
        get
        {
            return
        isBooked;
        }
    }
    public decimal NightlyRate
    {
        get
        {
            return nightlyRate;
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
        Console.WriteLine("Room Type:   " + roomType);
        Console.WriteLine("Nightly Rate:" + nightlyRate.ToString("C"));
        Console.WriteLine("Status:      " + (isBooked ? "Booked" : "Available"));
    }

    // Validation helpers
    private bool IsValidType(string type)
    {
        string[] validTypes = { "Standard", "Deluxe", "Suite" };
        return validTypes.Contains(type, StringComparer.OrdinalIgnoreCase);
    }

    private string NormalizeType(string type)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(type.ToLower());
    }
}
public class Booking
{
    // Fields
    private static int nextBookingID = 1;

    private int bookingID;
    private Guest guest;
    private Room room;
    private int nights;
    private decimal totalCost;

    // Constructor
    public Booking(Guest guest, Room room, int nights)
    {
        if (guest == null)
            throw new ArgumentNullException(nameof(guest));

        if (room == null)
            throw new ArgumentNullException(nameof(room));

        if (nights <= 0)
            throw new ArgumentException("Number of nights must be positive.");

        bookingID = nextBookingID++;
        this.guest = guest;
        this.room = room;
        this.nights = nights;
        this.totalCost = room.NightlyRate * nights;
    }

    // Properties
    public int BookingID { get { return bookingID; } }
    public Guest Guest { get { return guest; } }
    public Room Room { get { return room; } }
    public int Nights { get { return nights; } }
    public decimal TotalCost { get { return totalCost; } }

    // Method
    public void DisplayInfo()
    {
        Console.WriteLine("Booking ID:  " + BookingID);
        Console.WriteLine("Guest Name:  " + Guest.FullName);
        Console.WriteLine("Room Number: " + Room.RoomNumber);
        Console.WriteLine("Room Type:   " + Room.RoomType);
        Console.WriteLine("Nights:      " + Nights);
        Console.WriteLine("Total Cost:  " + TotalCost.ToString("C"));
    }
}
public class Hotel
{
    // Fields
    private List<Guest> guests;
    private List<Room> rooms;
    private List<Booking> bookings;

    public string HotelName { get; private set; }

    public Hotel(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(" Hotel name cannot be empty. ");

        HotelName = name;
        guests = new List<Guest>();
        rooms = new List<Room>();
        bookings = new List<Booking>();
    }

    // Guest Methods
    public void AddGuest(string name, string id)
    {
        if (guests.Any(g => g.NationalID == id))
        {
            Console.WriteLine("Guest with this ID already exists.");
            return;
        }

        guests.Add(new Guest(name, id));
    }

    public Guest FindGuest(string nationalID)
    {
        return guests.Find(g => g.NationalID == nationalID);
    }

    // Room Methods
    public void AddRoom(int number, string type, decimal rate)
    {
        if (rooms.Any(r => r.RoomNumber == number))
        {
            Console.WriteLine("Room already exists with this number.");
            return;
        }

        rooms.Add(new Room(number, type, rate));
    }

    // Booking Methods
    public void BookRoom(string nationalID, int roomNumber, int nights)
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

        Booking booking = new Booking(guest, room, nights);
        bookings.Add(booking);

        // update: increment guest booking counter
        guest.IncrementBookingCount();

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
        // Print summary before removing
        Console.WriteLine("Guest: "+ booking.Guest.FullName );
        Console.WriteLine("Room : "+ booking.Room.RoomNumber+" Nights "+ booking.Nights );
        Console.WriteLine("Cost:  " + booking.TotalCost.ToString("F3") + " OMR");
        booking.Room.CancelBooking();
        bookings.RemoveAll(b => b.BookingID == bookingID);
       

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

        Console.WriteLine("Bookings for "+guest.FullName);

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
    public void DisplayAvailableRooms()
    {
        Console.WriteLine("===== Available Rooms =====");

        var availableRooms = rooms.Where(r => !r.IsBooked).ToList();

        if (availableRooms.Count == 0)
        {
            Console.WriteLine("No available rooms.");
            return;
        }

        foreach (var room in availableRooms)
        {
            room.DisplayInfo();
            Console.WriteLine("------------------------");
        }
    }
    public void DisplayBookedRooms()
    {
        Console.WriteLine("===== Booked Rooms =====");

        var bookedRooms = rooms.Where(r => r.IsBooked).ToList();

        if (bookedRooms.Count == 0)
        {
            Console.WriteLine("No booked rooms.");
            return;
        }

        foreach (var room in bookedRooms)
        {
            room.DisplayInfo();
            Console.WriteLine("------------------------");
        }
    }
    public void DisplayAvailableRoomsByType(string type)
    {
        Console.WriteLine("===== Available "+ type +" Rooms =====");

        bool found = false;

        foreach (Room r in rooms)
        {
            if (!r.IsBooked && string.Equals(r.RoomType, type, StringComparison.OrdinalIgnoreCase))
            {
                r.DisplayInfo();
                Console.WriteLine("------------------------");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine(" No available rooms of type "+ type);
        }
    }
    public void DisplayAllGuests()
    {
        Console.WriteLine("===== All Registered Guests =====");

        if (guests.Count == 0)
        {
            Console.WriteLine("No guests registered.");
            return;
        }

        foreach (Guest g in guests)
        {
            g.DisplayInfo();
            Console.WriteLine("------------------------");
        }

        Console.WriteLine("Total registered guests: " + guests.Count);
    }
    public void FindMostExpensiveBooking()
    {
        Console.WriteLine("===== Most Expensive Booking =====");

        Booking max = null;

        foreach (Booking b in bookings)
        {
            if (max == null || b.TotalCost > max.TotalCost)
            {
                max = b;
            }
        }

        if (max != null)
        {
            max.DisplayInfo();
        }
        else
        {
            Console.WriteLine("No active bookings.");
        }
    }

    public void DisplayStatistics()
    {
        decimal total = bookings.Sum(b => b.TotalCost);
        decimal avg = bookings.Count > 0 ? total / bookings.Count : 0;
        Console.WriteLine("===== Hotel Statistics =====");

        Console.WriteLine("Hotel Name:    " + HotelName);
        Console.WriteLine("Total Guests:  " + guests.Count);
        Console.WriteLine("Total Rooms:   " + rooms.Count);
        Console.WriteLine("Total Bookings:" + bookings.Count);

        // Count how many rooms are currently booked in case  (IsBooked == true)
        int bookedRooms = rooms.Count(r => r.IsBooked);
        // Calculate available rooms by subtracting booked rooms from total rooms
        int availableRooms = rooms.Count - bookedRooms;

        Console.WriteLine("Booked Rooms: " + bookedRooms);
        Console.WriteLine("Available Rooms: " + availableRooms);

        Console.WriteLine("Total Guests Ever Created: " + Guest.GetTotalGuestsCreated());
        Console.WriteLine("Total Revenue:  " + total.ToString("F3") + " OMR");
        Console.WriteLine("Avg Cost/ Booking: " + avg.ToString("F3") + " OMR");

        Console.WriteLine("=============================");
    }
}

