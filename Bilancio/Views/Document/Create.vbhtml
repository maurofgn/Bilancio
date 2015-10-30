@ModelType Bilancio.Models.Document

<h2>@ViewBag.Title</h2>

@section Scripts

    @*This is for jquery UI, for Calender control*@
    @Scripts.Render("~/bundles/jqueryui")
    @Styles.Render("~/Content/themes/base/css")

    @*This is for JSON*@
    <script src="~/Scripts/json2.js" type="text/javascript"></script>

    @*These are for styling Control*@
    <link rel="stylesheet" type="text/css" href="~/Scripts/DataTables-1.10.9/css/jquery.dataTables.css" />

    @*These are for DataTables*@
    <script type="text/javascript" src="~/Scripts/DataTables-1.10.9/js/jquery.dataTables.js"></script>

    <script type="text/javascript">

    function DeleteRow() {
        //sostituito da inner function
    }

    var TABLE_ROW = {
        ID:{pos: 0, head: "ID"},
        Riga:{pos: 1, head: "Riga"},
        Conto_ID:{pos: 2, head: "Conto_ID"},
        Conto:{pos: 3, head: "Conto"},
        Importo:{pos: 4, head: "Importo"},
        Dare:{pos: 5, head: "Dare"},
        Nota:{pos: 6, head: "Nota"}
    };

    $(document).ready(
        function () {

            //table options define
            var table = $('#tblDocLine').DataTable(
                {
                    "bLengthChange": false,
                    "bFilter": false,
                    "bSort": false,
                    "bInfo": false,
                    "columnDefs": [{ "targets": [TABLE_ROW.ID.pos], "visible": false, "searchable": false }, { "targets": [TABLE_ROW.Conto_ID.pos], "visible": false, "searchable": false }],
                    "language": {
                        "decimal": ",",
                        "thousands": ".",
                        "lengthMenu": "_MENU_ righe per pagina",
                        "zeroRecords": "Non ci sono righe",
                        "info": "pagina _PAGE_ di _PAGES_",
                        "infoEmpty": "Non ci sono righe disponibili",
                        "emptyTable": "Tabella vuota",
                        "infoFiltered": "(filtrati su _MAX_ righe totali)",
                        "loadingRecords": "Sto caricando...",
                        "processing": "Sto elaborando...",
                        "search": "Cerca:",
                        "paginate": {
                            "first": "Primo",
                            "last": "Ultimo",
                            "next": "Successivo",
                            "previous": "Precedente"
                        },
                        "aria": {
                            "sortAscending": ": ordine ascendente",
                            "sortDescending": ": ordine discendente"
                        }
                    }
                }
            );

            //select/unselect one single row
            $('#tblDocLine tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    table.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            //remove one row from table
            $('#btnDelRow').click(function () {
                // prende i valori della riga selezionata e li assegna ai campi editabili prima di fare la delete della row, in modo che possa funzionare da update della riga
                var rowTable = table.row('.selected')

                assRow(rowTable, false)
            });

            //remove one row from table
            $('#btnUpdRow').click(function () {
                // prende i valori della riga selezionata (compreso l'id di riga) e li assegna ai campi editabili prima di fare la delete della row, in modo che possa funzionare da update della riga
                var rowTable = table.row('.selected')

                assRow(rowTable, true)
            });

            //add one row to table
            $('#btnAddRow').click(function () {
                //todo: prendere i valori della riga ed assegnarli ai campi riga prima di fare la delete, in modo che possa funzionare da update della riga


                var amount = Number($('#amountLine').val());
                
                if (amount == NaN || amount == 0) {
                    alert("L'importo è obbligatorio o importo non corretto");
                    return false;
                }

                var contoId = $('#Conto option:selected').val();
                if (!contoId) {
                    alert("Il conto è obbligatorio");
                    return false;
                }

                // Adding item to table
                table.row.add([
                    $('#LineId').val(),
                    $('#rowNr').val(),
                    $('#debit').val(),
                    $('#Conto option:selected').val(),
                    $('#Conto option:selected').text(),
                    $('#amountLine').val(),
                    $('#notaLine').val()
                ]).draw(false);

                // Making Editable text empty
                $('#LineId').val("")
                $('#rowNr').val("")
                $('#debit').val("")
                $('#Conto option:selected').attr("selected", null);
                $('#amountLine').val("")
                $('#notaLine').val("")
            });

            // $.datepicker.setDefaults($.datepicker.regional['it']);

            $('#dateReg').datepicker({
                dateFormat: "dd/mm/yy"
                //                    , yearRange: "2014:2015"
                //, regional:'it'
            });



            $('#dateDoc').datepicker({
                dateFormat: "dd/mm/yy"
                //                    , yearRange: "2014:2015"
                //, regional: 'it'
            });

        }
    );

    function assRow(rowTable, isUpdate) {

        if (isUpdate) {
            $('#LineId').val(rowTable.data()[TABLE_ROW.ID.pos])
            $('#debit').val(rowTable.data()[TABLE_ROW.Dare.pos])
            $('#DebitLabel').text(rowTable.data()[TABLE_ROW.Dare.pos])
        }
        else {
            $('#LineId').val("")
            $('#debit').val("")
            $('#DebitLabel').text("")
        }

        var contoID = rowTable.data()[TABLE_ROW.Conto_ID.pos]
        selectByValue(contoID)

        $('#rowNr').val(rowTable.data()[TABLE_ROW.Riga.pos])
        $('#amountLine').val(rowTable.data()[TABLE_ROW.Importo.pos])
        $('#notaLine').val(rowTable.data()[TABLE_ROW.Nota.pos])
        rowTable.remove().draw(false);
    }

        /**
        *imposta selected=true x la option con value txt e visualizza la label del conto con la specifica di conto dare o avere
        */
    function selectByValue(contoID) {

            //Le seguenti sono tutte funzionanti
            //$('#Conto option[value="' + contoID + '"]').attr('selected', true);

            //$('#Conto option')
            //    .filter(function () { return $.trim($(this).val()) == contoID; })
            //    .attr('selected', true);

            $('#Conto option')
               .filter(function (i, e) { return $.trim($(e).val()) == contoID; })
               .attr('selected', true);

            return labelDareAvereConto( contoID );            //  dare/avere del conto
        }

        /**
        * ritorna true se il conto è dare, false se è avere, undefined per altro
        */
        function labelDareAvereConto(contoId) {
            var debit = isDebit(contoId)
            $('#contoLabel').text(debit == undefined ? "Conto" : (debit ? "Conto Dare" : "Conto Avere"));
            return debit
        }

        function isDebit(id) {
            if (Number(id) <= 0)
                return undefined

            return $.inArray(Number(id), @Html.Raw(Json.Encode(ViewBag.ListAvereAccountChartId))) < 0;
        }

        // this function is used to add item to list table
        function Add() {
            //sostituito da inner function
        }

        //This function is used for sending data(JSON Data) to Controller
        function Document_save() {

            // Step 1: Read View Data and Create JSON Object

            // Creating row Json Object
            var row = { "ID": "", "Document_ID": "", "AccountChart_ID":"", "rowNr": "", "Dare": "", "amount": "", "note": "" };
            // Creating head Json Object
            var head = { "ID": "", "DocumentType_ID": "", "dateReg": "", "dateDoc": "", "docNr": "", "amount": "", "note": "", "documentRows": [] };

            // Set Sales Main Value
            head.ID = $("#DocumentId").val();
            head.dateReg = $("#dateReg").val();
            head.dateDoc = $("#dateDoc").val();
            head.docNr = $("#docNr").val();
            head.note = $("#note").val();
            head.amount = $("#amount").val();
            head.DocumentType_ID = $("#DocumentType_ID").val();

            var headId = typeof head.ID == "undefined" ? 0 : head.ID;

            // Getting Table Data from where we will fetch Sales Sub Record
            var rowsTable = $('#tblDocLine').DataTable();
            var data = rowsTable.rows().data()              //matrice di dati

            for (var i = 0; i < data.length; i++) {

                row.Document_ID = headId;
                // Set SalesSub individual Value
                row.ID = data[i][TABLE_ROW.ID.pos];
                row.rowNr = data[i][TABLE_ROW.Riga.pos];
                row.Dare = data[i][TABLE_ROW.Dare.pos];
                row.AccountChart_ID = data[i][TABLE_ROW.Conto_ID.pos];
                row.amount = data[i][TABLE_ROW.Importo.pos];
                row.note = data[i][TABLE_ROW.Nota.pos];

                // adding to head.documentRows List row
                head.documentRows.push(row);
                row = { "ID": "", "Document_ID": "", "AccountChart_ID": "", "rowNr": "", "Dare": "", "amount": "", "note": "" };
            }
            // Step 1: Ends Here

            // alert(JSON.stringify(head))

            // Set 2: Ajax Post
            // Here i have used ajax post for saving/updating information
            // url: 'http://localhost:10524/Sales/Create'

            $.ajax({
                url: '/Document/Create',
                data: JSON.stringify(head),
                type: 'POST',
                contentType: 'application/json;',
                dataType: 'json',
                success: function (result) {
                    if (result.Success == "1") {
                        window.location.href = "/Document/index";
                    }
                    else {
                        alert(result.ex);
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(jqXHR.statusText);
                }
            });
        }

</script>

    <script src="@Url.Content("~/Scripts/jquery.validate.min.js")" type="text/javascript"></script>
    <script src="@Url.Content("~/Scripts/jquery.validate.unobtrusive.min.js")" type="text/javascript"></script>
end section

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Testata Documento</legend>

        @If (Not IsNothing(Model) AndAlso Model.ID <> 0) Then
            @<input type="hidden" id="DocumentId" name="DocumentId" value="@Model.ID" />
        End If


        <div class="editor-label">
            @Html.LabelFor(Function(model) model.DocumentType_ID)
        </div>
        <div class="editor-field">
            @Html.DropDownListFor(Function(x) x.DocumentType_ID, ViewBag.documentTypes)
            @Html.ValidationMessageFor(Function(model) model.DocumentType_ID)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateReg)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.dateReg)
            @Html.ValidationMessageFor(Function(model) model.dateReg)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.dateDoc)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.dateDoc)
            @Html.ValidationMessageFor(Function(model) model.dateDoc)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.docNr)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.docNr)
            @Html.ValidationMessageFor(Function(model) model.docNr)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.note)
        </div>
        <div class="editor-field">
            @*@Html.EditorFor(Function(model) model.note, New With {.class = "textarea"})*@
            @Html.TextAreaFor(Function(model) model.note, New With {.class = "textarea"})
            @Html.ValidationMessageFor(Function(model) model.note)
        </div>

        <div class="editor-label">
            @Html.LabelFor(Function(model) model.amount)
        </div>
        <div class="editor-field">
            @Html.EditorFor(Function(model) model.amount)
            @Html.ValidationMessageFor(Function(model) model.amount)
        </div>
    </fieldset>

    @<br />
    @<fieldset>
        <legend>Riga Documento</legend>

        <input type="hidden" id="LineId" name="LineId" value="" />

        <label>riga :</label>
        @Html.TextBox("rowNr")

        @*<label id="Debit"></label>*@
@*        @Html.CheckBox("debit", False)*@

        <label id="contoLabel">Conto :</label>
        @Html.DropDownList("Conto", New SelectList(ViewBag.ListChart, "ID", "Name"), "-- scegliere un conto", New With {.onchange = "labelDareAvereConto($(this).val());"})

        <label>importo :</label>
        @Html.TextBox("amountLine")

        <label id="DebitLabel">Segno non definito</label>
        @Html.TextBox("debit", Nothing, New With {.ReadOnly = "readonly"})

        <br />
        <label>nota :</label>
        @Html.TextBox("notaLine")

        <input type="button" id="btnAddRow" value="Aggiungi" />
        <br />
        <br />

        <table class="display" id="tblDocLine">
            <thead><tr> <th>ID</th> <th>Riga</th> <th>Conto_ID</th> <th>Conto</th> <th>Importo</th> <th>Dare</th> <th>Nota</th></tr></thead>
            <tbody>
                @If (Not IsNothing(Model) AndAlso Not IsNothing(Model.documentRows)) Then
                    @For Each item In Model.documentRows
                        @<tr>
                            <td>@Html.DisplayTextFor(Function(i) item.ID)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.rowNr)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.AccountChart_ID)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.AccountChart.Name)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.amount)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.debit)</td>
                            <td>@Html.DisplayTextFor(Function(i) item.note)</td>
                        </tr>
                    Next
                End If
            </tbody>
        </table>

        <br />

        <input type="button" id="btnDelRow" value="Elimina riga selezionata" />
        <input type="button" id="btnUpdRow" value="Modifica riga selezionata" />


    </fieldset>

    @<input type="button" value="Salva Documento" onclick="Document_save()" />

End Using

<div>    @Html.ActionLink("Vai alla lista", "Index")  </div>
