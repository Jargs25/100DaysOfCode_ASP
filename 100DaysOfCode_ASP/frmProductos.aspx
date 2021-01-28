<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmProductos.aspx.cs" Inherits="_100DaysOfCode_ASP.frmProductos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="frmProductos" runat="server">
        <h1>CRUD Productos</h1>
        <asp:Panel ID="pnlDatos" runat="server" Width="400px" GroupingText="Datos">
            <asp:Image ID="imgProducto" runat="server" Width="100px" Height="100px" />
            <asp:Label ID="lblNodisponible" runat="server" Text="No Disponible" Visible="False"></asp:Label>
            <div>
                <asp:FileUpload ID="fudImagen" runat="server" />
            </div>
            <div>
                <asp:Label ID="lblCodigo" runat="server" Text="Código:"></asp:Label>
                <asp:TextBox ID="txtCodigo" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblNombre" runat="server" Text="Nombre:"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblCantidad" runat="server" Text="Cantidad:"></asp:Label>
                <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Label ID="lblPrecio" runat="server" Text="Precio:"></asp:Label>
                <asp:TextBox ID="txtPrecio" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" OnClick="btnAgregar_Click" />
                <asp:Button ID="btnModificar" runat="server" Text="Modificar" OnClick="btnModificar_Click" Enabled="False" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" Enabled="False" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlMensaje" runat="server" GroupingText="Mensaje del sitio" Visible="False" Width="400px">
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlRegistros" runat="server" Width="400px" GroupingText="Registros">
            <asp:GridView ID="gvRegistros" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="gvRegistros_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField DataField="id" HeaderText="ID" Visible="False" />
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
