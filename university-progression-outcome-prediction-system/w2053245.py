# I declare that my work contains no examples of misconduct, such as plagiarism, or collusion. 
# Any code taken from other sources is referenced within my code solution.
# Student ID: w2053245
# Date: 2022/12/13


# Import graphics module
from graphics import *

# initializing lists to store data
progress_list = []
trailer_list = []
retriver_list = []
excluded_list = []

# User decision variable
user_decision = "y"

# Credits validating list
valid_credits = [0,20,40,60,80,100,120]

# This function draws a bar graph using the provided credits
def draw_bar_graph(credits, headline):
    # Create a graphics window
    win = GraphWin("Histogram", 600, 500)
    
    # Set the title
    title_text = Text(Point(165, 20), "Histogram Results")
    title_text.setSize(20)
    title_text.setStyle("bold")
    title_text.setFill(color_rgb(99,99,99))
    title_text.draw(win)

    # Draw the x-axis
    Line(Point(25, 380), Point(560, 380)).draw(win)
    
    # Bar width
    bar_width = 110
    
    # Gap between columns
    column_gap = 13

    # Define labels for the bars
    labels = ["Progress", "Trailer", "Retriever", "Excluded"]

    # Define colors for the bars
    colors = [color_rgb(174, 248, 161), color_rgb(160, 198, 137), color_rgb(167, 188, 119), color_rgb(210, 182, 181)]
    
    for i in range(len(credits)):
        # initializing variables for better undestand
        value = credits[i]
        label = labels[i]
        bar_color = colors[i]
        
        # Calculate the coordinates for each bar
        x1 = 50 + i * (bar_width + column_gap)
        x2 = x1 + bar_width
        y1 = 380 - (value / max(credits)) * 320
        y2 = 380
        
        # Draw the bar with the specified color
        Rectangle(Point(x1, y1), Point(x2, y2)).draw(win).setFill(bar_color)
        
        # Display the data value at the top of the bar
        bar_top_value = Text(Point(x1 + bar_width / 2, y1 - 10), str(value))
        bar_top_value.setSize(14)
        bar_top_value.setStyle("bold")
        bar_top_value.setFill(color_rgb(122,136,153))
        bar_top_value.draw(win)
        
        # Add label below the bar
        bar_below_label = Text(Point(x1 + bar_width / 2, y2 + 12), label)
        bar_below_label.setSize(14)
        bar_below_label.setStyle("bold")
        bar_below_label.setFill(color_rgb(122,136,153))
        bar_below_label.draw(win)

    # Add a headline at the end of the graph
    headline_text = Text(Point(150, 440), headline)
    headline_text.setSize(18)
    headline_text.setStyle("bold")
    headline_text.setFill(color_rgb(122,136,153))
    headline_text.draw(win)

    # Wait for a mouse click before closing the window
    try:
        win.getMouse()
        win.close()
    except GraphicsError:
        # Ignore the error if the window is already closed
        pass  

# This function writes and prints the results to a file and the console
def write_and_print_results(progress_list, trailer_list, retriver_list, excluded_list):
    with open("Results.txt", "w") as file: 
        for i in range(0,len(progress_list),3):
            file.write(f"Progress - {progress_list[i]}, {progress_list[i+1]}, {progress_list[i+2]}\n")
            print(f"Progress - {progress_list[i]}, {progress_list[i+1]}, {progress_list[i+2]}")

        for i in range(0,len(trailer_list),3):
            file.write(f"Progress (module trailer) - {trailer_list[i]}, {trailer_list[i+1]}, {trailer_list[i+2]}\n")
            print(f"Progress (module trailer) - {trailer_list[i]}, {trailer_list[i+1]}, {trailer_list[i+2]}")

        for i in range(0,len(retriver_list),3):
            file.write(f"Module retriever - {retriver_list[i]}, {retriver_list[i+1]}, {retriver_list[i+2]}\n")
            print(f"Module retriever - {retriver_list[i]}, {retriver_list[i+1]}, {retriver_list[i+2]}")
    
        for i in range(0,len(excluded_list),3):
            file.write(f"Exclude - {excluded_list[i]}, {excluded_list[i+1]}, {excluded_list[i+2]}\n")
            print(f"Exclude - {excluded_list[i]}, {excluded_list[i+1]}, {excluded_list[i+2]}")

# Run the loop untill user_decision == "q"
while (user_decision.lower() != "q"):
    # Get and validate user input for pass
    while True:
        try:
            pass_credits = int(input("Please enter your credits at pass: "))
            if pass_credits in valid_credits:
                break
            print("Out of range.\n")
        except ValueError:
            print("Integer required\n")

    # Get and validate user input for defer
    while True:
        try:
            defer_credits = int(input("Please enter your credits at defer: "))
            if defer_credits in valid_credits:
                break
            print("Out of range.\n")
        except ValueError:
            print("Integer required\n")

    # Get and validate user input for fail
    while True:
        try: 
            fail_credits = int(input("Please enter your credits at fail: "))
            if fail_credits in valid_credits:
                break
            print("Out of range.\n")
        except ValueError:
            print("Integer required\n")

    # Validate total credits
    if pass_credits+defer_credits+fail_credits >120:
        print("Total incorrect.\n")
        continue

    # Determine outcome based on credits
    if pass_credits ==120:
        print("Progress\n")
        progress_list.extend([pass_credits, defer_credits, fail_credits])

    elif pass_credits == 100:
        print("Progress (module trailer)\n")
        trailer_list.extend([pass_credits, defer_credits, fail_credits])

    elif pass_credits in [40,20,0] and fail_credits in [80,100,120]:
        print("Exclude\n")
        excluded_list.extend([pass_credits, defer_credits, fail_credits])

    else:
        print("Do not Progress - module retriver\n")
        retriver_list.extend([pass_credits, defer_credits, fail_credits])
        
    # Ask user for another input
    print("Would you like to enter another set of data?")
    user_decision = input("Enter 'y' for yes or 'q' to quit and view results: ")
    # Validate user_decision
    while user_decision.lower() not in ["y","q"]:
        user_decision = input("Enter 'y' for yes or 'q' to quit and view results: ")
    print()


# Add credits to a list
credits = [int(len(progress_list)/3), int(len(trailer_list)/3), int(len(retriver_list)/3), int(len(excluded_list)/3)]

# Headline text with the total credits
headline = f"{credits[0]+credits[1]+credits[2]+credits[3]} outcomes in total."

# Call the draw_bar_graph function with the creadits and headline
draw_bar_graph(credits, headline)

# Writes and prints the results to a file and the console
write_and_print_results(progress_list, trailer_list, retriver_list, excluded_list)

# References 
# .extend() - https://reactgo.com/python-append-multiple-values-to-list/#appending-multiple-values-to-a-list
# graphics.py - https://mcsp.wartburg.edu/zelle/python/graphics/graphics.pdf 
# graphics.py try-except - https://lovelace.augustana.edu/q2a/index.php/4988/lab-7-question 
# file handling - https://www.programiz.com/python-programming/file-operation 
