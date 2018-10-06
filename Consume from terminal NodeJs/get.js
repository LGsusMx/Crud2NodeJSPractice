//Pass the id 
var id=1;
var Request = require("request");
Request.get("http://localhost:8084/api/tiendita/"+id, (error, response, body) => {
    if (error) {
        return console.dir(error);
    }
    console.dir(JSON.parse(body));
});