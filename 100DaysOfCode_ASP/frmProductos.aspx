<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="_100DaysOfCode_ASP.frmProductos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="margin:0;">
    <form id="frmProductos" runat="server" style="font-size:18px; font-family: Arial, sans-serif; background-color: #ffffdd; padding:10px;">
        <h1 style="margin:0 0 20px 0">CRUD Productos</h1>
        <asp:Panel ID="pnlDatos" runat="server" Width="370px" GroupingText="Datos" >
            <div style="width: 120px; display: inline-block;">
                <asp:Image ID="imgProducto" runat="server" Width="100px" Height="100px" />
                <asp:Label ID="lblNodisponible" runat="server" Text="No Disponible" Visible="False"></asp:Label>
                <div>
                    <asp:FileUpload ID="fudImagen" runat="server" onchange="loadImage()"/>
                    <input type="button" value="Subir Imagen" onclick="document.getElementById('fudImagen').click();" style="width: 100px; background-color: #ffff88; border-color: yellow; font-size: 14px;"/>
                </div>
            </div>
            <div style="width: 215px; display: inline-block; position:absolute">
                <div style="display: flex; margin-bottom: 2px; justify-content: space-between">
                    <asp:Label ID="lblCodigo" runat="server" Text="Código:"></asp:Label>
                    <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
                </div>
                <div style="display: flex; margin-bottom: 2px; justify-content: space-between">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
                </div>
                <div style="display: flex; margin-bottom: 2px; justify-content: space-between">
                    <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label>
                    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                </div>
                <div style="display: flex; margin-bottom: 2px; justify-content: space-between">
                    <asp:Label ID="lblPrecio" runat="server" Text="Precio:"></asp:Label>
                    <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
                </div>
                <div style="margin-top: 12px; text-align: center; display: flex; justify-content: space-between; flex-wrap: wrap;">
                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" Enabled="False" />
                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" Enabled="False" />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </asp:Panel>
        <style>
            fieldset{
                border-color: yellow;
            }
            fieldset legend {
                font-weight: bold;
            }
            #pnlDatos fieldset,
            #pnlRegistros fieldset {
                height: 200px;
            }
            #pnlMensaje fieldset {
                width: 420px;
                margin: 200px auto;
                padding: 10px 20px;
                position: relative;
            }
            #pnlMensaje:before{
                content: "";
                background-color: white;
                position: absolute;
                margin: 190px 438px;
                padding: 10px 20px;
                width: 450px;
                height: 168px;                
            }
            #pnlRegistros fieldset div {
                width: 400px;
                height: 100%;
                overflow: auto;
            }
            #gvRegistros a {
                color: #787817;
            }
            #gvRegistros th{
                font-weight: 100;
            }
            #gvRegistros td {
                padding: 0px 10px;
                font-size: 16px;
            }
        </style>
        <script>
            function loadImage() {
                var imagen = document.getElementById("fudImagen").files[0];
                var url = window.URL.createObjectURL(imagen);
                document.getElementById("imgProducto").src = url;
            }
        </script>
        <asp:Panel ID="pnlMensaje" runat="server" GroupingText="Mensaje del sitio" Visible="False">
            <div style="display:flex; justify-content:space-between;">
                <asp:Image ID="imgIcono" Width="100px" Height="100px" runat="server" />
                <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
            </div>
            <div style="text-align:center">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" />
                <asp:Button ID="btnNo" runat="server" Text="No" OnClick="btnNo_Click" Visible="False" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlRegistros" runat="server" Width="400px" GroupingText="Registros">
            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvRegistros_SelectedIndexChanged" DataKeyNames="id" SelectedRowStyle-BackColor="#ffff88">
                <Columns>
                    <asp:BoundField DataField="codigo" HeaderText="Codigo" />
                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                    <asp:BoundField DataField="precio" HeaderText="Precio" />
                    <asp:BoundField DataField="rutaImagen" HeaderText="RutaImagen" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </form>
</body>
</html>
