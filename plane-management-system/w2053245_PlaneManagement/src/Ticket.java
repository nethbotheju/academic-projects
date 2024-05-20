import java.io.*;

public class Ticket {

    // Attributes
    private char row;
    private int seat;
    private int price;
    private Person person;

    // Constructor
    public Ticket(char row, int seat, int price, Person person){
        this.row = row;
        this.seat = seat;
        this.price = price;
        this.person = person;
    }

    // Getters and Setters
    public void setRow(char row){
        this.row = row;
    }

    public char getRow(){
        return row;
    }

    public void setSeat(int seat){
        this.seat = seat;
    }

    public int getSeat(){
        return seat;
    }

    public void setPrice(int price){
        this.price = price;
    }

    public int getPrice(){
        return price;
    }

    public void setPerson(Person person){
        this.person = person;
    }

    public Person getPerson(){
        return person;
    }

    // Method to print Ticket information including person information
    public void printTicketInfo(){
        System.out.println("\nTicket Details: ");
        System.out.println("    Row: " + row);
        System.out.println("    Seat: " + seat);
        System.out.println("    Price: £" + price);

        System.out.println("\nPerson Details: ");
        person.printPersonInfo();
    }

    // Method to save txt file for ticket
    public void save(){
        // Create Tickets directory if it doesn't exist
        File myDirectory = new File("Tickets");
        if (!myDirectory.exists()){
            myDirectory.mkdirs();
        }

        // Create txt file with specific file name and save ticket details
        String file_name = "Tickets\\"+row+seat+".txt";
        try {
            FileWriter myWriter = new FileWriter(file_name);
            myWriter.write("Ticket Details: \n" + "    Row: " + row + "\n    Seat: " + seat + "\n    Price: £" + price);

            myWriter.write("\n\nPerson Details: ");
            myWriter.write("\n    Name: " + person.getName() + "\n    Surname: " + person.getSurname() + "\n    Email: " + person.getEmail());
            myWriter.close();
        }
        catch (IOException e){
            System.out.println("An error occurred." + e);
        }

    }
}
