using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _100DaysOfCode_ASP
{
    public partial class frmProductos : System.Web.UI.Page
    {
        modelo_productos mProductos = new modelo_productos();
        string rutaImagen;
        string nuevaImagen = "NoDisponible";


        protected void Page_Load(object sender, EventArgs e)
        {
            rutaImagen = Server.MapPath("~/Productos/");
            gvRegistros.DataSource = mProductos.BuscarProductos(new productos());
            gvRegistros.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                if (fudImagen.HasFile)
                {
                    string archivo = Path.GetFileName(fudImagen.FileName);
                    nuevaImagen = "http://"+HttpContext.Current.Request.Url.Authority + "/Productos/" + archivo;
                    fudImagen.SaveAs(rutaImagen + archivo);
                }

                mProductos.AgregarProducto(new productos("0000", txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()), nuevaImagen));

                gvRegistros.DataSource = mProductos.BuscarProductos(new productos());
                gvRegistros.DataBind();

                limpiarCampos();
            }
            else
            {
                pnlMensaje.Visible = true;
                lblMensaje.Text = "Por favor, verifique los campos.";
            }
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                id = (int)Session["id"];

                nuevaImagen = imgProducto.ImageUrl;

                if (fudImagen.HasFile)
                {
                    string archivo = Path.GetFileName(fudImagen.FileName);
                    nuevaImagen = "http://"+HttpContext.Current.Request.Url.Authority + "/Productos/" + archivo;
                    fudImagen.SaveAs(rutaImagen + archivo);
                }
                productos oProducto = new productos("0000", txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()), nuevaImagen);
                oProducto.id = id;
                mProductos.ModificarProducto(oProducto);

                gvRegistros.DataSource = mProductos.BuscarProductos(new productos());
                gvRegistros.DataBind();

                limpiarCampos();
            }
            else
            {
                pnlMensaje.Visible = true;
                lblMensaje.Text = "Por favor, verifique los campos.";
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            id = (int)Session["id"];

            nuevaImagen = imgProducto.ImageUrl;
            mProductos.EliminarProducto(id);

            if (nuevaImagen != "NoDisponible")
                File.Delete(rutaImagen + nuevaImagen.Split('/')[4]);

            gvRegistros.DataSource = mProductos.BuscarProductos(new productos());
            gvRegistros.DataBind();

            limpiarCampos();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (btnBuscar.Text != "Limpiar")
            {
                pnlMensaje.Visible = false;
                lblMensaje.Text = "";
                gvRegistros.DataSource = mProductos.BuscarProductos(new productos(txtCodigo.Text.Trim(), txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim() != "" ? txtCantidad.Text.Trim() : "0"), Convert.ToDouble(txtPrecio.Text.Trim() != "" ? txtPrecio.Text.Trim() : "0"), nuevaImagen));
            }
            else
            {
                gvRegistros.DataSource = mProductos.BuscarProductos(new productos());
                limpiarCampos();
            }
                gvRegistros.DataBind();
        }

        int id = -1;
        protected void gvRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvRegistros.SelectedIndex;
            id = Convert.ToInt32(gvRegistros.Rows[index].Cells[1].Text);

            Session["id"] = id;

            if (id > -1)
            {
                txtCodigo.Text = gvRegistros.Rows[index].Cells[2].Text;
                txtNombre.Text = gvRegistros.Rows[index].Cells[3].Text;
                txtCantidad.Text = gvRegistros.Rows[index].Cells[4].Text;
                txtPrecio.Text = gvRegistros.Rows[index].Cells[5].Text;
                nuevaImagen = gvRegistros.Rows[index].Cells[6].Text;

                if (nuevaImagen != "NoDisponible")
                {
                    imgProducto.ImageUrl = nuevaImagen;
                }
                else
                {
                    lblNodisponible.Visible = true;
                }

                btnAgregar.Enabled = false;
                btnModificar.Enabled = true;
                btnEliminar.Enabled = true;
                btnBuscar.Text = "Limpiar";
            }

        }

        public bool sonValidos()
        {
            if (txtNombre.Text.Trim() == "" ||
            txtCantidad.Text.Trim() == "" ||
            txtPrecio.Text.Trim() == "")
                return false;
            return true;
        }
        public void limpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtCantidad.Text = "";
            txtPrecio.Text = "";
            nuevaImagen = "NoDisponible";
            imgProducto.ImageUrl = null;
            id = -1;
            btnAgregar.Enabled = true;
            btnModificar.Enabled = false;
            btnEliminar.Enabled = false;
            btnBuscar.Text = "Buscar";
            pnlMensaje.Visible = false;
            lblMensaje.Text = "";
            lblNodisponible.Visible = false;
        }
    }
}