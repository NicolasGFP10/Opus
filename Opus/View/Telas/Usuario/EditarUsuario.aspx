<%@ Page Title="Editar dados - Opus" Language="C#" MasterPageFile="~/View/Site.Master" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="Opus.View.Telas.Usuario.EditarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <h2>Editar dados</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Nome</label>
        <asp:TextBox ID="tbxNome" type="text" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">E-mail</label>
        <asp:TextBox ID="tbxEmail" type="email" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Telefone</label>
        <asp:TextBox ID="tbxTelefone" class="form-control" runat="server" CssClass="form-control telefone"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Senha</label>
        <asp:TextBox ID="tbxSenha" type="password" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Foto de perfil (deixe vazio para não alterar)</label>
        <asp:FileUpload ID="fuImagem" runat="server" CssClass="form-control" />
    </div>

    <br />

    <center>
        <h5>(Lembrando, ao editar os dados, você irá sair de sua conta)</h5>
    </center>

    <br />

    <center>
        <asp:Button runat="server" ID="btnEnviar" type="button" class="btn cor-roxa" OnClick="btnEnviar_Click" Text="Confirmar" />
    </center>

    <script src="../../JS/AjustarCaracteres.js"></script>
</asp:Content>
