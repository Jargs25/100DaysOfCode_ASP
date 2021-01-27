using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _100DaysOfCode_ASP
{
    public partial class frmProductos : System.Web.UI.Page
    {
        List<productos> lstProductos = new List<productos>();
        string rutaImagen;
        string nuevaImagen = "NoDisponible";


        protected void Page_Load(object sender, EventArgs e)
        {
            rutaImagen = Server.MapPath("~/Productos/");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (sonValidos())
            {
                lstProductos = (List<productos>)Session["Productos"];
                if (lstProductos == null)
                    lstProductos = new List<productos>();

                if (fudImagen.HasFile)
                {
                    string archivo = Path.GetFileName(fudImagen.FileName);
                    nuevaImagen = "http://"+HttpContext.Current.Request.Url.Authority + "/Productos/" + archivo;
                    fudImagen.SaveAs(rutaImagen + archivo);
                }

                lstProductos.Add(new productos("0000", txtNombre.Text.Trim(), Convert.ToInt32(txtCantidad.Text.Trim()), Convert.ToDouble(txtPrecio.Text.Trim()), nuevaImagen));

                Session["Productos"] = lstProductos;

                gvRegistros.DataSource = lstProductos;
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
                lstProductos = (List<productos>)Session["Productos"];
                id = (int)Session["id"];

                productos oProducto = lstProductos[id];
                nuevaImagen = oProducto.rutaImagen;

                if (fudImagen.HasFile)
                {
                    string archivo = Path.GetFileName(fudImagen.FileName);
                    nuevaImagen = "http://"+HttpContext.Current.Request.Url.Authority + "/Productos/" + archivo;
                    fudImagen.SaveAs(rutaImagen + archivo);
                }

                oProducto.codigo = "000000";
                oProducto.nombre = txtNombre.Text.Trim();
                oProducto.cantidad = Convert.ToInt32(txtCantidad.Text.Trim());
                oProducto.precio = Convert.ToDouble(txtPrecio.Text.Trim());
                oProducto.rutaImagen = nuevaImagen;

                Session["Productos"] = lstProductos;

                gvRegistros.DataSource = lstProductos;
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
            lstProductos = (List<productos>)Session["Productos"];
            id = (int)Session["id"];

            productos oProducto = lstProductos[id];
            nuevaImagen = oProducto.rutaImagen;
            lstProductos.RemoveAt(id);
            if (nuevaImagen != "NoDisponible")
            File.Delete(rutaImagen + nuevaImagen.Split('/')[4]);

            Session["Productos"] = lstProductos;

            gvRegistros.DataSource = lstProductos;
            gvRegistros.DataBind();

            limpiarCampos();

        }
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            lstProductos = (List<productos>)Session["Productos"];
            if (lstProductos == null)
                lstProductos = new List<productos>();

            if (btnBuscar.Text != "Limpiar")
            {
                List<productos> filtro = new List<productos>();

                for (int i = 0; i < lstProductos.Count; i++)
                {
                    productos oProducto = lstProductos[i];

                    if (oProducto.codigo.Contains(txtCodigo.Text.Trim()) &&
                        oProducto.nombre.ToUpper().Contains(txtNombre.Text.Trim().ToUpper()) &&
                        oProducto.cantidad.ToString().Contains(txtCantidad.Text.Trim()) &&
                        oProducto.precio.ToString().Contains(txtPrecio.Text.Trim()))
                    {
                        filtro.Add(oProducto);
                    }
                }

                pnlMensaje.Visible = false;
                lblMensaje.Text = "";
                gvRegistros.DataSource = filtro;
            }
            else
            {
                gvRegistros.DataSource = lstProductos;
                limpiarCampos();
            }
                gvRegistros.DataBind();
        }

        int id = -1;
        protected void gvRegistros_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstProductos = (List<productos>)Session["Productos"];

            id = gvRegistros.SelectedIndex;
            Session["id"] = id;

            if (id > -1)
            {
                productos oProducto = lstProductos[id];
                txtCodigo.Text = oProducto.codigo;
                txtNombre.Text = oProducto.nombre;
                txtCantidad.Text = oProducto.cantidad.ToString();
                txtPrecio.Text = oProducto.precio.ToString();

                if(oProducto.rutaImagen != "NoDisponible")
                {
                    imgProducto.ImageUrl = oProducto.rutaImagen;
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