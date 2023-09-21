const express = require("express");
const mysql = require("mysql");

const app = express();
const port = 3000; // Puedes cambiar el puerto según tus preferencias

// Configura la conexión a la base de datos
const db = mysql.createConnection({
  host: "localhost", // Cambia esto si tu base de datos no está en localhost
  user: "root", // Nombre de usuario de la base de datos
  password: "", // Contraseña de la base de datos
  database: "pf_server", // Nombre de la base de datos
});

// Conectar a la base de datos
db.connect((err) => {
  if (err) {
    console.error("Error al conectar a la base de datos: " + err.message);
  } else {
    console.log("Conexión exitosa a la base de datos.");
  }
});

// Middleware para procesar JSON
app.use(express.json());

// Ruta para recibir datos del controlador
app.post("/actualizar_estado", (req, res) => {
  const { id_controlador, id_sensor, valor } = req.body;

  // Insertar datos en la base de datos (tabla "parking")
  const sql =
    "INSERT INTO parking (id_controlador, id_sensor, valor) VALUES (?, ?, ?)";
  db.query(sql, [id_controlador, id_sensor, valor], (err, result) => {
    if (err) {
      console.error(
        "Error al insertar datos en la base de datos: " + err.message
      );
      res.status(500).send("Error interno del servidor.");
    } else {
      console.log("Datos insertados en la base de datos.");
      res.status(200).send("Datos insertados correctamente.");
    }
  });
});

// Iniciar el servidor
app.listen(port, () => {
  console.log(`Servidor Node.js en funcionamiento en el puerto ${port}`);
});
