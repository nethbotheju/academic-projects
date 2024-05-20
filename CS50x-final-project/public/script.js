// Function to fetch and display todos
function getTodos() {
  $.get('/api/todos', function (data) {
    const taskList = $('#taskList');
    taskList.empty();
    data.forEach(todo => {
      const listItem = $('<li>');
      const circleButton = $('<span onclick="deleteTask(' + todo.id + ')" class="circle-button"></span>');
      const taskText = $('<span onclick="deleteTask(' + todo.id + ')" class="task-text">>').text(todo.task);
      listItem.append(circleButton, taskText);
      if (todo.completed) {
        listItem.addClass('completed');
      }
      taskList.append(listItem);
    });
  });
}

// Function to add a todo
function addTask() {
  const taskInput = $('#taskInput');
  const task = taskInput.val();
  $.post('/api/todos', { task: task }, function (data) {
    taskInput.val('');
    getTodos();
    checkInput(); // Check the input after adding a task
  });
}



function checkInput() {
  const taskInput = $('#taskInput');
  const addButton = $('#addButton');

  // Disable the button if the input value is empty
  addButton.prop('disabled', taskInput.val().trim() === '');
}



// Function to delete a todo
function deleteTask(id) {
  $.ajax({
    url: '/api/todos/' + id,
    type: 'DELETE',
    success: function (data) {
      getTodos();
      showNotification('Task completed!');
    }
  });
}

// Function to show a notification
function showNotification(message) {
  const notification = $('<div class="notification"></div>').text(message);
  $('body').append(notification);

  // Fade in the notification over 1 second, keep it visible for 1 second, then fade out over 1 second
  notification.fadeIn(300).delay(500).fadeOut(1000, function () {
    $(this).remove();
  });
}

// Fetch and display todos on page load
$(document).ready(function () {
  getTodos();
});
