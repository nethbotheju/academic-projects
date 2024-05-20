# Todo List Web App

Welcome to our ToDo List Web App, a comprehensive tool designed to help you manage your tasks effectively. Accessible from any device and at any time, this application ensures that you stay on top of your tasks, no matter where you are. It is built with Node.js, Express, and SQLite, providing a basic API for managing a todo list.

#### Video Demo:  <https://youtu.be/_WPwdnxXOao>

## How it works

The application leverages the power of the Express framework to handle HTTP requests, and SQLite as a database to store the todo items. The `body-parser` middleware is used to parse incoming request bodies in a middleware before the handlers, ensuring smooth and efficient data handling.

Upon launch, the application sets up an Express server and defines the port it will listen to. It then initializes a SQLite database and creates a `todos` table if it doesn't exist, preparing the groundwork for storing and managing your tasks.

The application provides three main API endpoints:

1. `GET /api/todos`: This endpoint retrieves all the todo items from the database and returns them as a JSON response. This allows you to view all your tasks at a glance.

2. `POST /api/todos`: This endpoint adds a new todo item to the database. The new task is provided in the request body. This means you can easily add new tasks to your list.

3. `DELETE /api/todos/:id`: This endpoint deletes a todo item from the database. The ID of the task to be deleted is provided in the URL parameters. This allows you to remove completed or unwanted tasks from your list.

The application uses the `sqlite3` library to interact with the SQLite database. The `db.run` method is used to execute SQL queries, and the `db.all` method is used to fetch all rows from the `todos` table.

Finally, the application starts listening for incoming requests on the defined port, ready to help you manage your tasks.

## Running the application

To run the application, you need to have Node.js installed. You can then install the required dependencies with `npm install` and start the server with `npm start`. The server will be accessible at `http://localhost:3000`, allowing you to start managing your tasks right away.

## Future improvements

While this application provides a basic functionality for managing a todo list, there are several potential improvements and features that could be added. These include user authentication for personalized task management, task prioritization to help you focus on important tasks, and more. We are constantly working to improve this application and make it more useful for you. Stay tuned for future updates!

## Detailed Features

### User-friendly Interface

The application boasts a user-friendly interface that is easy to navigate. The tasks are displayed in a clear and organized manner, making it easy for you to manage your tasks.

### Cross-platform Accessibility

The application is accessible from any device, be it a computer, a tablet, or a smartphone. This means you can manage your tasks anytime, anywhere.

### Real-time Updates

The application updates in real time, ensuring that you always have the most up-to-date information about your tasks.

### Secure Data Storage

The application uses SQLite for data storage, ensuring that your data is stored securely and reliably.

### Efficient Task Management

The application provides efficient task management features, such as the ability to add, delete, and view tasks. This makes it easy for you to manage your tasks and stay organized.

## Conclusion

In conclusion, our ToDo List Web App is a powerful tool that can help you manage your tasks effectively. With its user-friendly interface, cross-platform accessibility, real-time updates, secure data storage, and efficient task management features, it is the perfect tool for anyone looking to stay organized and on top of their tasks. Try it out today and see the difference it can make in your productivity!
