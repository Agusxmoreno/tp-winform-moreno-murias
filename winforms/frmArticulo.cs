﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;

namespace winforms
{
    public partial class frmArticulos : Form
    {
        private List<Articulo> listaArticulos;

        public frmArticulos()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cargar();
        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.ImagenUrl);
            cargarDescripcion(seleccionado.Descripcion);
            cargarCategoria(seleccionado.Categoria.Descripcion);
            cargarMarca(seleccionado.Marca.Descripcion);
        }

        private void cargar()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulos = negocio.listar();
                dgvArticulos.DataSource = listaArticulos;
                dgvArticulos.Columns["Id"].Visible = false;
                dgvArticulos.Columns["Categoria"].Visible = false;
                dgvArticulos.Columns["Descripcion"].Visible = false;
                dgvArticulos.Columns["Marca"].Visible = false;
                dgvArticulos.Columns["ImagenUrl"].Visible = false;
                cargarImagen(listaArticulos[0].ImagenUrl);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception)
            {
                pbxArticulos.Load("http://ichno.org/dokuwiki/lib/exe/fetch.php?cache=&w=500&h=500&tok=f62c23&media=characters:placeholder.png");
            }
        }

        private void cargarDescripcion(string desc)
        {
            try
            {
                txtDescripcion.Text = desc;
           
            }
            catch (Exception)
            {
                txtDescripcion.Text = "Artículo sin descripción.";
            }
        }

        private void cargarCategoria(string desc)
        {
            try
            { 
                txtCategoria.Text = desc;
            }
            catch (Exception)
            {
                txtCategoria.Text = "Artículo sin descripción.";
            }
        }
        private void cargarMarca(string desc)
        {
            try
            {
                txtMarca.Text = desc;
            }
            catch (Exception)
            {
                txtMarca.Text = "Artículo sin descripción.";
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaArticulo alta = new frmAltaArticulo();
            alta.ShowDialog();
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem; 
            frmAltaArticulo modificar = new frmAltaArticulo(seleccionado);
            modificar.ShowDialog();
            cargar();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            DialogResult dialogresult = MessageBox.Show("¿Deseas eliminar este artículo?", "Eliminar", MessageBoxButtons.YesNo);

            Articulo seleccionado;
            seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
            frmAltaArticulo eliminar = new frmAltaArticulo(seleccionado);

            if(dialogresult == DialogResult.Yes)
            {
                negocio.eliminar(seleccionado);
            }

            cargar();

        }
    }
}
