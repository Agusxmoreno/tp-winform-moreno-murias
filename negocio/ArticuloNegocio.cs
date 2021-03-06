using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()

        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos(); 

            try
            {
                datos.setearConsulta("Select A.ID, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, A.IdMarca, A.IdCategoria from Articulos A, Marcas M, CATEGORIAS C where A.IdMarca = M.Id AND A.IdCategoria = C.Id");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.ID = (int)datos.Lector["Id"];
                    if(!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    
                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    

                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> listar(string nombre, int num)

        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                if (num == 1)
                {
                    datos.setearConsulta("Select A.ID, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, A.IdMarca, A.IdCategoria from Articulos A, Marcas M, CATEGORIAS C where A.IdMarca = M.Id AND A.IdCategoria = C.Id and nombre like '%" + @nombre + "%'");
                    datos.setearParametro("@nombre", nombre);
                    datos.ejecutarLectura();
                }
                else if (num == 2)
                {
                    datos.setearConsulta("Select A.ID, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, A.IdMarca, A.IdCategoria from Articulos A, Marcas M, CATEGORIAS C where A.IdMarca = M.Id AND A.IdCategoria = C.Id and M.Descripcion like '%" + @nombre + "%'");
                    datos.setearParametro("@marca", nombre);
                    datos.ejecutarLectura();
                }
                else
                {
                    datos.setearConsulta("Select A.ID, Codigo, Nombre, A.Descripcion, M.Descripcion as Marca, C.Descripcion as Categoria, ImagenUrl, Precio, A.IdMarca, A.IdCategoria from Articulos A, Marcas M, CATEGORIAS C where A.IdMarca = M.Id AND A.IdCategoria = C.Id and c.descripcion like '%" + @nombre + "%'");
                    datos.setearParametro("@categoria", nombre);
                    datos.ejecutarLectura();
                }
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    aux.ID = (int)datos.Lector["Id"];
                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.Codigo = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descripcion = (string)datos.Lector["Descripcion"];

                    aux.Marca = new Marca();
                    aux.Marca.ID = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Categoria();
                    aux.Categoria.ID = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];


                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = (decimal)datos.Lector["Precio"];

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)values(@Codigo, @Nombre, @Descripcion, @idMarca, @idCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion);
                datos.setearParametro("@idMarca", nuevo.Marca.ID);
                datos.setearParametro("@idCategoria", nuevo.Categoria.ID);
                datos.setearParametro("@ImagenUrl", nuevo.ImagenUrl);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificar(Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS SET CODIGO = @CODIGO, NOMBRE = @NOMBRE, DESCRIPCION = @DESCRIPCION, IDMARCA = @IDMARCA, IDCATEGORIA = @IDCATEGORIA, IMAGENURL = @IMAGENURL, PRECIO = @PRECIO WHERE ID = @ID");
                datos.setearParametro("@Codigo", art.Codigo);
                datos.setearParametro("@Nombre", art.Nombre);
                datos.setearParametro("@Descripcion", art.Descripcion);
                datos.setearParametro("@idMarca", art.Marca.ID);
                datos.setearParametro("@idCategoria", art.Categoria.ID);
                datos.setearParametro("@ImagenUrl", art.ImagenUrl);
                datos.setearParametro("@Precio", art.Precio);
                datos.setearParametro("@ID", art.ID);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
        
        
        public void eliminar (Articulo art)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE ID = @ID");
                datos.setearParametro("@ID", art.ID);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }



        }

    }
}
