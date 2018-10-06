//Note: The description of the product  must be on  the db first.
var Request = require("request");
Request.post({
    "headers": { "content-type": "application/json" },
    "url": "http://localhost:8084/api/tiendita/",
    "body": JSON.stringify([
        { "Descripcion": "Teclado Gaming", "Cantidad": "2" },
        { "Descripcion": "Audifonos Piston 2 Xiaomi", "Cantidad": "3" },
        { "Descripcion": "Mouse Optico 2.0", "Cantidad": "1" }
    ])
}, (error, response, body) => {
    if (error) {
        return console.dir(error);
    }
    console.dir("Metodo exitoso");
});