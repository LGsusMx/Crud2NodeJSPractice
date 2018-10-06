//Pass the date to search separated (day, month, year)
var dia=04;
var mes=10;
var anio=2018;
var Request = require("request");
Request.get("http://localhost:8084/api/tiendita?anio="+anio+"&mes="+mes+"&dia="+dia, (error, response, body) => {
    if (error) {
        return console.dir(error);
    }
    console.dir(JSON.parse(body));
});