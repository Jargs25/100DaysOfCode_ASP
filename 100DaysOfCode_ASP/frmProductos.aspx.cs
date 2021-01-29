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
            agregarEstilos();
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
                mostrarMensaje("Por favor, verifique los campos.",0,0);
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
                mostrarMensaje("Por favor, verifique los campos.");
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            mostrarMensaje("La siguiente acción no podrá deshacerse ¿Desea continuar?", 1, 1);
            Session["accion"] = "Eliminar";
        }
        private void EliminarProducto()
        {
            id = (int)Session["id"];

            if (imgProducto.ImageUrl != "")
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
            id = Convert.ToInt32(gvRegistros.DataKeys[index].Value);

            Session["id"] = id;

            if (id > -1)
            {
                txtCodigo.Text = gvRegistros.Rows[index].Cells[1].Text;
                txtNombre.Text = gvRegistros.Rows[index].Cells[2].Text;
                txtCantidad.Text = gvRegistros.Rows[index].Cells[3].Text;
                txtPrecio.Text = gvRegistros.Rows[index].Cells[4].Text;
                nuevaImagen = gvRegistros.Rows[index].Cells[5].Text;

                if (nuevaImagen != "NoDisponible")
                {
                    imgProducto.ImageUrl = nuevaImagen;
                    lblNodisponible.Visible = false;
                }
                else
                {
                    imgProducto.ImageUrl = "";
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
            gvRegistros.SelectedIndex = -1;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
            lblMensaje.Text = "";

            string accion = "";
            if(Session["accion"] != null)
            {
                accion = Session["accion"].ToString();
                ejecutarAccion(accion);
                Session["accion"] = null;
            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
            lblMensaje.Text = "";
        }

        protected void mostrarMensaje(string mensaje)
        {
            pnlMensaje.Visible = true;
            imgIcono.Visible = false;
            lblMensaje.Text = mensaje;
            btnAceptar.Text = "Aceptar";
            btnNo.Visible = false;
            lblMensaje.Attributes.Remove("style");
            lblMensaje.Attributes.Add("style","width: 400px; height: 100px; padding: 0px 8px; display: flex; justify-content: center; align-items: center;");
        }
        protected void mostrarMensaje(string mensaje, int opcion)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;

            switch (opcion)
            {
                case 1:
                    btnAceptar.Text = "Si";
                    btnNo.Visible = true;
                    break;
                default:
                    btnAceptar.Text = "Aceptar";
                    btnNo.Visible = false;
                    break;
            }
            lblMensaje.Attributes.Add("style", "width: 400px; height: 100px; padding: 0px 8px; display: flex; text-align: center; align-items: center;");
        }
        protected void mostrarMensaje(string mensaje, int opcion, int icono)
        {
            string rutaIcono = "http://" + HttpContext.Current.Request.Url.Authority + "/Iconos/";

            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            imgIcono.Visible = true;

            switch (opcion)
            {
                case 1:
                    btnAceptar.Text = "Si";
                    btnNo.Visible = true;
                    break;
                default:
                    btnAceptar.Text = "Aceptar";
                    btnNo.Visible = false;
                    break;
            }

            switch (icono)
            {
                case 1:
                    imgIcono.ImageUrl = rutaIcono + "warning.png";
                    break;
                default:
                    imgIcono.ImageUrl = rutaIcono +"info.png";
                    break;
            }
        }
        protected void ejecutarAccion(string accion)
        {
            switch (accion)
            {
                case "Eliminar":
                    EliminarProducto();
                    break;
            }
        }

        protected void agregarEstilos()
        {
            pintarPaneles();
            pintarImagen();
            pintarNoDisponible();
            pintarFileUpload();
            pintarTextBox();
            pintarBotones();
            pintarMensaje();
        }
        protected void pintarPaneles()
        {
            pnlDatos.Attributes.CssStyle.Add("display", "inline-block");
            pnlRegistros.Attributes.CssStyle.Add("display", "inline-block");
        }
        protected void pintarImagen()
        {
            imgProducto.Attributes.CssStyle.Add("background-color", "white");
        }
        protected void pintarNoDisponible()
        {
            lblNodisponible.Attributes.Add("style", "position: absolute; left: 33px; top: 130px; width: 90px; display: inline-block; text-align: center;");
        }
        protected void pintarFileUpload()
        {
            fudImagen.Attributes.Add("style", "display:none");
        }
        protected void pintarTextBox()
        {
            string style = "width:124px; display:inline-block;";
            txtCodigo.Attributes.Add("style", style);
            txtNombre.Attributes.Add("style", style);
            txtCantidad.Attributes.Add("style", style);
            txtPrecio.Attributes.Add("style", style);
        }
        protected void pintarBotones()
        {
            string style = "width: 104px; background-color: #ffff88; border-color: yellow; margin-top: 4px; margin-right: 2px; font-size: 14px;";
            btnAgregar.Attributes.Add("style", style);
            btnModificar.Attributes.Add("style", style);
            btnEliminar.Attributes.Add("style", style);
            btnBuscar.Attributes.Add("style", style);
        }
        protected void pintarMensaje()
        {
            pnlMensaje.Attributes.Add("style", "display: inline-block; position: absolute; width: 100%; height: 100%; top: 0; left: 0; background-color: rgba(0, 0, 0, 0.4);");
            imgIcono.Attributes.CssStyle.Add("background-color", "white");
            lblMensaje.Attributes.Add("style", "width: 300px; height: 100px; padding: 0px 8px; display: flex; justify-content: center; align-items: center;");
            string style = "width: 100px; background-color: #ffff88; border-color: yellow;";
            btnAceptar.Attributes.Add("style", style);
            btnNo.Attributes.Add("style", style);
        }
    }
}