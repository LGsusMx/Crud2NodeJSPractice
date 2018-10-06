using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using WebApiCRUD2.Models;

namespace WebApiCRUD2.Controllers
{
    public class TienditaController : ApiController
    {
        [HttpGet]
        public IEnumerable<ViewModel> GetId(int id)
        {
            try
            {
                List<ViewModel> _list = new List<ViewModel>();
                string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM view_Catalogo WHERE Descripcion=(SELECT Nombre FROM Producto WHERE Id=" + id + ")", con);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ViewModel _user = new ViewModel()
                        {
                            Id = long.Parse(rdr["ID"].ToString()),
                            Descripcion = rdr["Descripcion"].ToString(),
                            Cantidad = int.Parse(rdr["Cantidad"].ToString()),
                            Total = decimal.Parse(rdr["Total"].ToString()),
                            Fecha = DateTime.Parse(rdr["Fecha"].ToString()),
                            Estatus = int.Parse(rdr["Estatus"].ToString()),
                        };
                        _list.Add(_user);
                    }
                }
                return _list;
            }
            catch (Exception m)
            {
                throw new Exception(m.Message);
            }
            finally
            {
            }
        }

        [HttpGet]
        public IEnumerable<ViewModel> GetDate(int dia,int mes, int anio)
        {
            try
            {
                List<ViewModel> _list = new List<ViewModel>();
                string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
                using (SqlConnection con = new SqlConnection(conn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT  * FROM view_Catalogo  WHERE '" + (anio+"-"+mes+"-"+0+dia) + "'<= Fecha and Fecha < '" + (anio + "-" + mes + "-" + 0+(dia+1)) + "'", con);
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        ViewModel _user = new ViewModel()
                        {
                            Id = long.Parse(rdr["ID"].ToString()),
                            Descripcion = rdr["Descripcion"].ToString(),
                            Cantidad = int.Parse(rdr["Cantidad"].ToString()),
                            Total = decimal.Parse(rdr["Total"].ToString()),
                            Fecha = DateTime.Parse(rdr["Fecha"].ToString()),
                            Estatus = int.Parse(rdr["Estatus"].ToString()),
                        };
                        _list.Add(_user);
                    }
                }
                return _list;
            }
            catch (Exception m)
            {
                throw new Exception(m.Message);
            }
            finally
            {
            }
        }

        public IHttpActionResult PostNewSell(List<ViewModel> Compra)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalidad Data.");
            string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                long id = 0;
                SqlDataReader reader = null;
                SqlCommand sc = new SqlCommand("INSERT INTO Compra(Total) VALUES(" + 0 + ")", con);
                sc.ExecuteNonQuery();
                sc = null;
                sc = new SqlCommand("Select max(Id) as ID from Compra;", con);
                reader = sc.ExecuteReader();
                if (reader.Read())
                {
                    id = (long)reader["ID"];
                }
                con.Close();
                foreach (var item in Compra)
                {
                    con.Open();
                    SqlCommand Insert = new SqlCommand("INSERT INTO Venta_Detalle(IdVe,IdPr,Cantidad,PrecioT) VALUES (" + id + ",(SELECT Id FROM Producto WHERE Nombre='" + item.Descripcion + "')," + item.Cantidad + ",(SELECT Precio FROM Producto WHERE Nombre='" + item.Descripcion + "'))", con);
                    Insert.ExecuteNonQuery();
                    Insert = null;
                    con.Close();
                }
            }
            return Ok();
        }

        public IHttpActionResult PutACorrection(ViewModel correcion)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalidad Data.");

            string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand sc = new SqlCommand("UPDATE Venta_Detalle SET Cantidad=" + correcion.Cantidad + " WHERE Id=" + correcion.Id, con);
                sc.ExecuteNonQuery();
                con.Close();
            }
            return Ok();
        }
        //Using 2 routes for delete different things
        [Route("api/tiendita/product")]
        public IHttpActionResult DeleteProduct(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalidad Data.");

            string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand sc = new SqlCommand("UPDATE Venta_Detalle SET Estatus=" + 0 + "Where Id=" + id, con);
                sc.ExecuteNonQuery();
                con.Close();
            }
            return Ok();
        }
        [Route("api/tiendita/recipe")]
        public IHttpActionResult DeleteRecipe(long id)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalidad Data.");

            string conn = ConfigurationManager.ConnectionStrings["MiBD"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand sc = new SqlCommand("UPDATE Compra SET Estatus=" + 0 + "Where Id=" + id, con);
                sc.ExecuteNonQuery();
                con.Close();
            }
            return Ok();
        }
    }
}