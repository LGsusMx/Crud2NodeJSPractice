//Pass id 
var id=7;
var Request = require("request");
//Request.delete("http://localhost:8084/api/tiendita/recipe?id="+id, (error, response, body) => { //uncoment this for delete a full sale
 Request.delete("http://localhost:8084/api/tiendita/product?id="+id, (error, response, body) => { //uncoment this for delete a part of a sale
    if (error) {
        return console.dir(error);
    }
    console.dir("Metodo exitoso");
});