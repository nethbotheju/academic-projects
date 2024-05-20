const express = require('express');
const sqlite3 = require('sqlite3');
const bodyParser = require('body-parser');

const app = express();
const port = 3000;

const db = new sqlite3.Database('todos.db');

// Create table if not exists
db.run(`
  CREATE TABLE IF NOT EXISTS todos (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    task TEXT
  )
`);

app.use(bodyParser.urlencoded({ extended: true }));
app.use(express.static('public'));

// API to get all todos
app.get('/api/todos', (req, res) => {
  db.all('SELECT * FROM todos', (err, rows) => {
    if (err) {
      res.status(500).json({ error: err.message });
      return;
    }
    res.json(rows);
  });
});

// API to add a todo
app.post('/api/todos', (req, res) => {
  const task = req.body.task;
  db.run('INSERT INTO todos (task) VALUES (?)', [task], function (err) {
    if (err) {
      res.status(500).json({ error: err.message });
      return;
    }
    res.json({ id: this.lastID, task: task });
  });
});

// API to delete a todo
app.delete('/api/todos/:id', (req, res) => {
  const id = req.params.id;
  db.run('DELETE FROM todos WHERE id = ?', id, function (err) {
    if (err) {
      res.status(500).json({ error: err.message });
      return;
    }
    res.json({ message: 'Deleted', changes: this.changes });
  });
});

app.listen(port, () => {
  console.log(`Server running on http://localhost:${port}`);
});
