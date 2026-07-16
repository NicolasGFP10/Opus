<%@ page title="Tornar-se autônomo - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="CadastrarAutonomo.aspx.cs" inherits="Opus.View.Telas.Autonomo.CadastrarAutonomo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Torne-se um autônomo</h2>
    </center>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">CNPJ</label>
        <asp:TextBox ID="tbxCNPJ" type="text" class="form-control" runat="server" CssClass="form-control cnpj"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">E-mail corporativo (não obrigatório)</label>
        <asp:TextBox ID="tbxEmail" type="email" class="form-control" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlInput1" class="form-label">Telefone corporativo (não obrigatório)</label>
        <asp:TextBox ID="tbxTelefone" class="form-control" runat="server" CssClass="form-control telefone"></asp:TextBox>
    </div>

    <div class="mb-3">
        <label for="exampleFormControlTextarea1" class="form-label">Fale um pouco sobre você</label>
        <asp:TextBox ID="tbxDescricao" class="form-control" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
    </div>

    <center>
        <asp:Button runat="server" ID="btnEnviar" type="button" class="btn btn-dark cor-roxa" OnClick="btnCadastro" Text="Cadastrar" />
    </center>

    <script src="../../JS/AjustarCaracteres.js"></script>

</asp:Content>
