import java.util.*;
import java.io.File;

public class w2053245_PlaneManagement {

    // Scanner object for user inputs 
    private static Scanner input = new Scanner(System.in);

    // Create a list to store Ticket objects
    private static Ticket[] tickets = new Ticket[52];

    // Variable to store number of booked tickets
    private static int booked_tickets = 0;

    // Declaring 2D array to represent the seats
    private static int[][] seats;

    public static void main(String [] args){
        System.out.println("\nWelcome to the Plane Management System.\n");

        // Assigning 2D array
        seats = new int[4][];
        seats[0] = new int[14];
        seats[1] = new int[12];
        seats[2] = new int[12];
        seats[3] = new int[14];

        // Main loop for the menu
        while(true){
            // Print menu options
            System.out.println("*************************************************");
            System.out.println("*                MENU OPTIONS                   *");
            System.out.println("*************************************************");
            System.out.println("     1) Buy a seat");
            System.out.println("     2) Cancel a seat");
            System.out.println("     3) Find first available seat");
            System.out.println("     4) Show seating plan");
            System.out.println("     5) Print tickets information and total sales");
            System.out.println("     6) Search ticket");
            System.out.println("     0) Quit");
            System.out.println("*************************************************\n");

            // Get user option
            int option;
            while(true) {
                System.out.print("Please select an option: ");
                if(input.hasNextInt()) {
                    option = input.nextInt();
                    if (option < 0 || option > 6) {
                        System.out.println("Please enter valid option.\n");
                    }else{
                        break;
                    }
                }else{
                    System.out.println("Please enter a numerical value for the option.\n");
                    input.next();
                }
            }

            // Execute the selected option using switch
            switch (option) {
                case 1:
                    buy_seat();
                    break;
                case 2:
                    cancel_seat();
                    break;
                case 3:
                    find_first_available();
                    break;
                case 4:
                    show_seating_plan();
                    break;
                case 5:
                    print_tickets_info();
                    break;
                case 6:
                    search_ticket();
                    break;
                case 0:
                    input.close();
                    System.out.println("\nThank you for using the Plane Management System.\nNow exiting...");
                    System.exit(0);
            }
            System.out.println();
        }
    }

    // buy_seat method to buy a seat
    public static void buy_seat(){
        // Get row and seat number using inputs method
        int[] inputs = inputs();

        // Initializing row_number and seat_number from inputs
        int row_number = inputs[0];
        int seat_number = inputs[1];

        // Check whether the seat is unreserved
        if(seats[row_number][seat_number] == 0){

            // Book the seat
            seats[row_number][seat_number] = 1;
            System.out.println("\nYour seat has been booked!\n");

            // Get user details for the ticket
            System.out.println("To complete your booking, please enter your personal details for the ticket.");
            System.out.print("Enter your name: ");
            String name = input.next();
            System.out.print("Enter your surname: ");
            String surname = input.next();
            System.out.print("Enter your email: ");
            String email = input.next();

            // Calculate the actual row letter and seat number
            char row_letter = (char) ('A' + row_number);
            seat_number += 1;

            // Calculate the price based on the seat number
            int price;
            if(seat_number<6){
                price = 200;
            }
            else if(seat_number<10){
                price = 150;
            }
            else{
                price = 180;
            }

            // Create person and ticket objects
            Person person = new Person(name, surname, email);
            Ticket ticket =  new Ticket(row_letter, seat_number, price, person);

            // Add that ticket object to the tickets list
            for (int i = 0; i < tickets.length; i++) {
                if(tickets[i] == null) {
                    tickets[i] = ticket;
                    booked_tickets += 1;
                    break;
                }
            }

            // Call save method in ticket class to save the ticket information as a text file
            ticket.save();

            System.out.println("\nYour ticket has been successfully booked and saved!\n");
        }
        else{
            System.out.println("\nSorry, this seat is already sold. Please try selecting another seat.\n");
        }
    }

    // cancel_seat method to cancel a seat
    public static void cancel_seat(){
        // Get row and seat number using inputs method
        int[] inputs = inputs();

        // Initializing row_number and seat_number from inputs
        int row_number = inputs[0];
        int seat_number = inputs[1];

        // Check whether the seat is reserved
        if(seats[row_number][seat_number] == 1){
            // Cancel the seat
            seats[row_number][seat_number] = 0;

            // Calculate the actual row letter and seat number
            char row_letter = (char) ('A' + row_number);
            seat_number += 1;

            // Remove that ticket from the tickets list
            for (int i = 0; i < tickets.length; i++) {
                if(tickets[i] != null) {
                    if (tickets[i].getRow() == row_letter && tickets[i].getSeat() == seat_number) {
                        tickets[i] = null;
                        booked_tickets -= 1;
                        break;
                    }
                }
            }

            // Delete the ticket txt file
            String file_name = "Tickets\\"+row_letter+seat_number+".txt";
            File file = new File(file_name);
            if (file.exists()){
                file.delete();
                System.out.println("\nThe seat has been successfully canceled!\n");
            }else{
                System.out.println("\nThe seat has been successfully canceled, but the ticket information file could not be located.\n");
            }
        }
        else{
            System.out.println("\nThe seat you selected is currently unreserved!\n");
        }
    }

    // find_first_available method to find the first available seat
    public static void find_first_available(){
        boolean selected = false;

        int row = -1;
        int column = -1;
        // Find the first available seat using a for loop
        for(int i =0; i<seats.length && !selected; i++){
            for(int j =0; j<seats[i].length && !selected; j++){
                if(seats[i][j] == 0){
                    row = i;
                    column = j+1;
                    selected = true;
                }
            }
        }

        // If all the seat are sold
        if(row<0 || column<0) {
            System.out.println("\nUnfortunately, all seats are currently booked on this flight!\n");
        }
        else{
            // Print the first available seat
            char row_letter = (char) (65 + row);
            System.out.print("\nThe first available seat is: ");
            System.out.println("" + row_letter + column + "\n");
        }
    }

    // show_seat_plan method to show the seating plan
    public static void show_seating_plan(){
        System.out.println();
        // Using a for loop, print the availability of seats using characters
        for(int i =0; i<seats.length; i++){
            for(int j =0; j<seats[i].length; j++){
                if(seats[i][j] == 0){
                    System.out.print("O "); // Seat is available
                }
                else{
                    System.out.print("X "); // Seat is sold
                }
            }
            System.out.println();
        }
        System.out.println();
    }

    // search_ticket method to search for a ticket
    public static void search_ticket(){
        // Get row and seat number using inputs method
        int[] inputs = inputs();

        // Initializing row_letter and seat_number from inputs to the actual row letter and seat number
        char row_letter = (char) (65+ inputs[0]);
        int seat_number = inputs[1] +1;

        boolean selected = false;

        // Using a for loop, find the ticket with the row and seat from the tickets array
        for(Ticket ticket: tickets){
            if(ticket != null) {
                if (ticket.getRow() == row_letter && ticket.getSeat() == seat_number) {
                    // Print that ticket's details using printTicketInfo method
                    ticket.printTicketInfo();
                    System.out.println();
                    selected = true;
                    break;
                }
            }
        }

        // If no ticket is found display a message
        if(!selected){
            System.out.println("\nThe seat you selected is currently unreserved!\n");
        }
    }

    // print_tickets_info method to print total ticket amount with each ticket price
    public static void print_tickets_info(){
        int sum =0;
        String print = "";

        // If there are booked tickets
        if(booked_tickets != 0) {
            // Using a for loop, calculate total amount and save details to the print variable using string concatenate
            for (Ticket ticket : tickets) {
                if(ticket != null) {
                    sum += ticket.getPrice();
                    print += "" + ticket.getRow() + ticket.getSeat() + " = £" + ticket.getPrice() + " + ";
                }
            }

            // Delete last 3 character and display values
            print = print.substring(0, print.length() - 3);
            System.out.println("\n£" + sum + " (" + print + ")\n");
        }else{
            System.out.println("\nThere are currently no tickets booked yet!\n");
        }
    }

    // inputs method to get the row and seat number from the user
    public static int[] inputs(){

        // Get row_letter and validate it
        char row_letter;
        while(true){
            System.out.print("Enter the row letter: ");
            String row = input.next();
            if(row.length() != 1){
                System.out.println("Please enter valid row letter ('A' or 'B' or 'C' or 'D').\n");
            }
            else {
                row_letter = row.charAt(0);
                row_letter = Character.toUpperCase(row_letter);
                if (row_letter < 65 || row_letter > 68) {
                    System.out.println("Please enter valid row letter ('A' or 'B' or 'C' or 'D').\n");
                } else {
                    break;
                }
            }
        }

        // Get seat_number and validate it based on the row
        int seat_number;
        while(true) {
            System.out.print("Enter the seat number: ");
            if(input.hasNextInt()) {
                seat_number = input.nextInt();
                if (row_letter == 'A' || row_letter == 'D') {
                    if (seat_number <= 0 || seat_number > 14) {
                        System.out.println("Please enter valid seat number (This row has only 14 seats).\n");
                        continue;
                    }
                } else {
                    if (seat_number <= 0 || seat_number > 12) {
                        System.out.println("Please enter valid seat number (This row has only 12 seats).\n");
                        continue;
                    }
                }
                break;
            }else{
                System.out.println("Please enter a numerical value for the seat number.\n");
                input.next(); 
            }
        }

        // Create int array to return row_letter and seat_number
        int[] return_values = new int[2];

        // Assigning the system side row number and seat number to the array
        return_values[0] = row_letter - 65; 
        return_values[1] = seat_number -1;

        return return_values;
    }
}