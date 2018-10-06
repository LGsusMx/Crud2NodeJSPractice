//Only updates an item of the sale in its quantity
var Request = require("request");
Request.put({
    "headers": { "content-type": "application/json" },
    "url": "http://localhost:8084/api/tiendita/",
    "body": JSON.stringify({
        "Id": "16", "Cantidad": "1"
    })
}, (error, response, body) => {
    if (error) {
        return console.dir(error);
    }
    console.dir("Metodo exitoso");
});