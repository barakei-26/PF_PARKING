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

  // Comprobar si ya existe una fila para el par id_controlador y id_sensor
  const sqlSelect =
    "SELECT valor FROM parking WHERE id_controlador = ? AND id_sensor = ?";
  db.query(sqlSelect, [id_controlador, id_sensor], (err, results) => {
    if (err) {
      console.error("Error al consultar la base de datos: " + err.message);
      res.status(500).send("Error interno del servidor.");
      return;
    }

    if (results.length === 0) {
      // No existe una fila para este par id_controlador y id_sensor, crear una nueva fila
      const sqlInsert =
        "INSERT INTO parking (id_controlador, id_sensor, valor) VALUES (?, ?, ?)";
      db.query(sqlInsert, [id_controlador, id_sensor, valor], (err, result) => {
        if (err) {
          console.error(
            "Error al insertar datos en la base de datos: " + err.message
          );
          res.status(500).send("Error interno del servidor.");
        } else {
          console.log("Nueva fila insertada en la base de datos.");
          res.status(200).send("Nueva fila insertada correctamente.");
        }
      });
    } else {
      const existingValue = results[0].valor;

      // Si el valor es diferente, actualizar la fila existente
      if (valor !== existingValue) {
        const sqlUpdate =
          "UPDATE parking SET valor = ? WHERE id_controlador = ? AND id_sensor = ?";
        db.query(
          sqlUpdate,
          [valor, id_controlador, id_sensor],
          (err, result) => {
            if (err) {
              console.error(
                "Error al actualizar datos en la base de datos: " + err.message
              );
              res.status(500).send("Error interno del servidor.");
            } else {
              console.log("Fila actualizada en la base de datos.");
              res.status(200).send("Fila actualizada correctamente.");
            }
          }
        );
      } else {
        // El valor es el mismo, no es necesario hacer nada
        console.log("El valor es el mismo, no se realizó ninguna acción.");
        res
          .status(200)
          .send("El valor es el mismo, no se realizó ninguna acción.");
      }
    }
  });
});

// Iniciar el servidor
app.listen(port, () => {
  console.log(`Servidor Node.js en funcionamiento en el puerto ${port}`);
});
