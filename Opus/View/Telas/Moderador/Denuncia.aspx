<%@ page title="Denuncias - Opus" language="C#" masterpagefile="~/View/Site.Master" autoeventwireup="true" codebehind="Denuncia.aspx.cs" inherits="Opus.View.Telas.Moderador.Denuncia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <center>
        <h2>Denuncias</h2>
    </center>

    <br />
    <br />

    <div class="mb-3 row">
        <label for="staticEmail" class="col-sm-2 col-form-label">Filtrar por</label>

        <div class="col-sm-10">
            <asp:DropDownList id="ddlFiltro"
                AutoPostBack="true"
                runat="server"
                CssClass="form-select"
                Style="width: 300px;"
                OnSelectedIndexChanged="Selection_Change">

                <asp:ListItem Selected="True" Value="0">Mostrar Todos</asp:ListItem>
                <asp:ListItem Selected="False" Value="1">Mostrar Pendentes</asp:ListItem>
                <asp:ListItem Selected="False" Value="2">Mostrar Resolvidos</asp:ListItem>

            </asp:DropDownList>
        </div>
    </div>

    <br />

    <asp:Repeater
        ID="rptDenuncias"
        runat="server"
        OnItemCommand="rptDenuncias_ItemCommand">

        <itemtemplate>

            <div class="card-denuncia shadow-sm p-3 mb-5 bg-body-tertiary rounded">


                <div class="hstack gap-3">
                    <div class="p-2">

                        <h4>Título: 
                            <%#: Eval("Titulo") %>
                        </h4>

                    </div>

                    <div class="p-2">
                        <h6>Data da denúncia: 
                            <%#: Eval("DataEnvio") %>
                        </h6>
                    </div>

                    <div class="p-2 ms-auto">
                        <p>
                            Status:
                            <asp:Label
                                ID="lblStatus"
                                runat="server"
                                Text='<%# Eval("Status") %>' />
                        </p>
                    </div>

                    <div class="p-2 ms-auto">

                        <!-- Button trigger modal -->
                        <button type="button" class="btn btn-danger" data-bs-toggle="modal" data-bs-target="#exampleModal">
                            Alterar o Status
                        </button>

                        <!-- Modal -->
                        <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h1 class="modal-title fs-5" id="exampleModalLabel">Alterar os status</h1>
                                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                                    </div>
                                    <div class="modal-body">
                                        <h6>Você tem certeza que deseja fazer isso?</h6>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Não</button>

                                        <asp:LinkButton
                                            ID="btnGerenciar"
                                            runat="server"
                                            Text="Sim"
                                            CssClass="btn btn-danger"
                                            CommandName="Gerenciar"
                                            CommandArgument='<%# Eval("ID") %>' />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <h6>Denuncia de: 
                        <%#: Eval("Categoria") %>
                </h6>

                <p>
                    Descrição: 
                    <%#: Eval("Descricao") %>
                </p>

                <!-- EVIDÊNCIAS -->

                <h5>Imagens da denúncia</h5>

                <asp:Repeater
                    runat="server"
                    DataSource='<%# Eval("Fotos") %>'>

                    <itemtemplate>

                        <asp:Image
                            runat="server"
                            Width="150"
                            ImageUrl='<%# Eval("CaminhoFoto") %>'
                            CssClass="padding-lat-10" />

                    </itemtemplate>

                </asp:Repeater>

                <!-- DENUNCIANTE E DENUNCIADO -->


                <div class="hstack gap-3 padding-top-20">
                    <div class="p-2">
                        <h5>Denunciante</h5>
                    </div>
                    <div class="p-2">
                        <asp:Image
                            runat="server"
                            Width="80"
                            CssClass="rounded-circle object-fit-cover"
                            Style="width: 50px; height: 50px;"
                            ImageUrl='<%# "~/Uploads/Usuario/" + Eval("ImagemDenunciante") %>' />
                    </div>
                    <div class="p-2">
                        <p>
                            <%#: Eval("NomeDenunciante") %>
                        </p>
                    </div>
                </div>

                <div class="hstack gap-3">

                    <div class="p-2">
                        <h5>Denunciado</h5>
                    </div>

                    <div class="p-2">
                        <asp:Image
                            runat="server"
                            Width="80"
                            CssClass="rounded-circle object-fit-cover"
                            Style="width: 50px; height: 50px;"
                            ImageUrl='<%# "~/Uploads/Usuario/" + Eval("ImagemDenunciado") %>' />
                    </div>

                    <div class="p-2">
                        <p>
                            <%#: Eval("NomeDenunciado") %>
                        </p>
                    </div>

                </div>
            </div>

        </itemtemplate>

    </asp:Repeater>

</asp:Content>
